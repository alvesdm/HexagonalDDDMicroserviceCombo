using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using RabbitHole;

namespace Host.Api.Requests.Commands
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterApiCommands(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IAmApiCommand).GetTypeInfo().Assembly)
                .Where(t=> typeof(IAmApiCommand).IsAssignableFrom(t))
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(IAmApiCommandHandler).GetTypeInfo().Assembly)
                .Where(t => typeof(IAmApiCommandHandler).IsAssignableFrom(t))
                .AsImplementedInterfaces();

            return builder;
        }

        public static ContainerBuilder RegisterApiBus(this ContainerBuilder builder, IConfiguration configuration)
        {
            builder.Register(p =>
            {
                var appName = $"{configuration.GetValue<string>(Constants.Configuration.Service.Name)}";//.{Assembly.GetEntryAssembly().GetName().Name}";
                var _rabbitMQSettings = new Infrastructure.Configuration.Settings.RabbitMQ.Connection(configuration);
                var client = RabbitHole.Factories.ClientFactory
                    .Create(appName)
                    .WithConnection(c =>
                        c.WithHostName($"{_rabbitMQSettings.Host}:{_rabbitMQSettings.Port}")
                            .WithVirtualHost(_rabbitMQSettings.VirtualHost)
                            .WithPassword(_rabbitMQSettings.Password)
                            .WithUserName(_rabbitMQSettings.User));

                return client;
            }).As<IClient>().InstancePerLifetimeScope();


            builder.Register(p =>
                {
                    var _queuesSettings = new List<Infrastructure.Configuration.Settings.RabbitMQ.Queue>();
                    configuration.GetSection(Constants.Configuration.Broker.Queues).Bind(_queuesSettings);

                    return _queuesSettings;
                }).As<IList<Infrastructure.Configuration.Settings.RabbitMQ.Queue>>()
                .SingleInstance();

            builder.RegisterAssemblyTypes(typeof(IAmApiBus).GetTypeInfo().Assembly)
                .Where(t => typeof(IAmApiBus).IsAssignableFrom(t))
                .AsImplementedInterfaces();

            //builder.Register(p=>new TodoBus(p.Resolve<IClient>())).As<IAmTodoBus>();
            return builder;
        }
    }
}
