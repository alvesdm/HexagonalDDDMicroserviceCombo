using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Application.Hosts.Ports;
using Autofac;
using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using PeterKottas.DotNetCore.WindowsService;
using PeterKottas.DotNetCore.WindowsService.Interfaces;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using RabbitHole;
using Connection = Infrastructure.Configuration.Settings.RabbitMQ.Connection;
using Queue = Infrastructure.Configuration.Settings.RabbitMQ.Queue;

namespace Host.Worker.Core
{
    public class Bootstrap<TService> 
        where TService : IMicroService, IHaveContainer
    {
        public static Bootstrap<TService> Instance => new Bootstrap<TService>();

        private Action<ContainerBuilder> _registerDependenciesAction;
        private IContainer _container;
        private string _serviceName;
        private string _serviceFullName;
        private RetryPolicy _retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetry(new[]
            {
                TimeSpan.FromMilliseconds(50),
                TimeSpan.FromMilliseconds(100),
                TimeSpan.FromMilliseconds(150)
            });

        private CircuitBreakerPolicy _circuitBreakerPolicy = Policy
            .Handle<Exception>()
            .CircuitBreaker(1, TimeSpan.FromMilliseconds(500));

        private IConfiguration _configurationRoot;
        private Connection _rabbitMQSettings;
        private readonly MessageHandlerDictionary _messageHandlerDictionary = new MessageHandlerDictionary();
        private IClient _consumerBrokerClient;
        private IEnumerable<Queue> _queuesSettings;

        public void Run()
        {
            LoadConfiguration();
            InitializeBroker();
            InitializeContainer();
            using (var scope = _container.BeginLifetimeScope())
            {
                ServiceRunner<TService>.Run(config =>
                {
                    config.SetName(_serviceFullName);

                    var name = config.GetDefaultName();
                    config.Service(serviceConfig =>
                    {
                        var myService = scope.Resolve<TService>();
                        myService.Container = _container;
                        serviceConfig.ServiceFactory((arguments, controller) => myService);
                        serviceConfig.OnStart((service, extraArguments) =>
                        {
                            Console.WriteLine($"Service {name} started");
                            service.Start();
                        });

                        serviceConfig.OnStop(async service =>
                        {
                            Console.WriteLine($"Service {name} stopped");
                            Shutdown();
                            service.Stop();
                        });

                        serviceConfig.OnInstall(service =>
                        {
                            Console.WriteLine($"Service {name} installed");
                        });

                        serviceConfig.OnUnInstall(service =>
                        {
                            Console.WriteLine($"Service {name} uninstalled");
                        });

                        serviceConfig.OnPause(service =>
                        {
                            Console.WriteLine($"Service {name} paused");
                        });

                        serviceConfig.OnContinue(service =>
                        {
                            Console.WriteLine($"Service {name} continued");
                        });

                        serviceConfig.OnError(e =>
                        {
                            Console.WriteLine($"Service {name} errored with exception : {e.Message}");
                        });
                    });
                });
            }
        }

        private void Shutdown()
        {
            _consumerBrokerClient.Shutdown();
        }

        private void LoadConfiguration()
        {
            _configurationRoot = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            if(string.IsNullOrEmpty(_serviceName))
                _serviceName = _configurationRoot.GetValue<string>(Constants.Configuration.ServiceName);

            _serviceFullName = $"{_serviceName}.{typeof(TService).FullName}";

            _queuesSettings = new List<Queue>();
            _configurationRoot.GetSection(Constants.Configuration.Broker.Queues).Bind(_queuesSettings);
        }

        private void InitializeContainer()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<TService>();
            containerBuilder.RegisterInstance(_configurationRoot).As<IConfiguration>();
            RegisterWithContainer(containerBuilder);
            _container = containerBuilder.Build();
        }

        private void InitializeBroker()
        {
            _rabbitMQSettings = new Connection(_configurationRoot);

            _consumerBrokerClient = RabbitHole.Factories.ClientFactory
                .Create(_serviceFullName)
                .WithConnection(c =>
                    c.WithHostName($"{_rabbitMQSettings.Host}:{_rabbitMQSettings.Port}")
                        .WithVirtualHost(_rabbitMQSettings.VirtualHost)
                        .WithPassword(_rabbitMQSettings.Password)
                        .WithUserName(_rabbitMQSettings.User));

            InitializeConsumerBroker();
        }

        private void InitializeConsumerBroker()
        {
            _messageHandlerDictionary
                .Dictionary
                .ToList()
                .ForEach(CreateConsumer);
        }

        private void CreateConsumer(KeyValuePair<Type, Type> messageHandlerMap)
        {
            Type[] typeArgs = { messageHandlerMap.Key, messageHandlerMap.Value };
            this.GetType()
                .GetMethod("BindConsumerToHandler", BindingFlags.NonPublic | BindingFlags.Instance)
                .MakeGenericMethod(typeArgs)
                .Invoke(this, null);
        }

        private void BindConsumerToHandler<TMessage, TMessageHandler>()
            where TMessage : IMessage
            where TMessageHandler : IMessageHandler<TMessage>, new()
        {
            var exchangeName = _queuesSettings.First(q => q.Message.Equals(typeof(TMessage).FullName)).Exchange;
            var queueName = typeof(TMessage).FullName;
            _consumerBrokerClient
                .DeclareExchange(c => c.WithName(exchangeName))
                .DeclareQueue(q => q.WithName(queueName))
                .Consume<TMessage>(c =>
                    c.WithExchange(exchangeName)
                        .WithQueue(queueName)
                        .WhenReceive(
                            (eventingBasicConsumer, basicDeliverEventArgs, message, e) =>
                            {
                                new TMessageHandler().Handle(message);
                                return Task.FromResult(true);
                            }));
        }

        private void RegisterWithContainer(ContainerBuilder containerBuilder)
        {
            _registerDependenciesAction(containerBuilder);
        }

        public Bootstrap<TService> WithMessageHandlers(Action<MessageHandlerDictionary> messageHandlerBuilder)
        {
            messageHandlerBuilder(_messageHandlerDictionary);
            return this;
        }

        public Bootstrap<TService> WithDependencies(Action<ContainerBuilder> dependenciesBuilder)
        {
            _registerDependenciesAction = dependenciesBuilder;
            return this;
        }

        public Bootstrap<TService> WithName(Func<string> name)
        {
            _serviceName = name();
            return this;
        }

        public Bootstrap<TService> WithRetryPolicy(Func<RetryPolicy> retryPolicy)
        {
            _retryPolicy = retryPolicy();
            return this;
        }

        public Bootstrap<TService> WithCircuitBreakerPolicy(Func<CircuitBreakerPolicy> circuitBreakerPolicy)
        {
            _circuitBreakerPolicy = circuitBreakerPolicy();
            return this;
        }
    }

    public class MessageHandlerDictionary
    {
        public IReadOnlyDictionary<Type, Type> Dictionary => _dictionary.ToImmutableDictionary();

        private readonly Dictionary<Type, Type> _dictionary = new Dictionary<Type, Type>();

        public void Add<TMessage, THandler>()
            where TMessage : IMessage
            where THandler : IMessageHandler<TMessage>
        {
            _dictionary.Add(typeof(TMessage), typeof(THandler));
        }
    }
}