using System;
using FluentCheck.HealthCheck.WorkerHealthCheck;
using Host.Worker.Core;
using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;

namespace Host.Worker
{
    internal class Service : ServiceBase
    {
        readonly WorkerClient _workerClient = new WorkerClient();
        private readonly string _pingBackUri;

        public Service(MyTypeA service, MyTypeB serviceB, IConfiguration configuration)
        {
            PollerInterval = 1200; //not necessary just for example purpose Default 1000
            _pingBackUri =
                $"{configuration.GetValue<string>(Constants.Configuration.Api.BaseUri)}{configuration.GetValue<string>(Constants.Configuration.Api.PingBackUri)}";

            Console.WriteLine($"Service Version:{configuration.GetValue<string>(Constants.Configuration.Service.Version)}");
        }

        protected override void Poller()
        {
            _workerClient.PingBack(_pingBackUri); //we don't want to wait for it, so no need to await it ;)

            Console.WriteLine($"Pinged back to Api Health check @'{_pingBackUri}', at {DateTime.Now:o}\n");
        }
    }

    public class MyTypeA
    {
        private readonly MyTypeB _service;

        public MyTypeA(MyTypeB service)
        {
            _service = service;
        }
        public string Name { get; set; }
    }

    public class MyTypeB
    {
        public string Name => "aaaa";
    }
}