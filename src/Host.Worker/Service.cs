using System;
using Host.Worker.Core;
using Microsoft.Extensions.Configuration;

namespace Host.Worker
{
    internal class Service : ServiceBase
    {
        public Service(MyTypeA service, MyTypeB serviceB, IConfiguration configuration)
        {
            PollerInterval = 1500;

            Console.WriteLine($"Service Version:{configuration.GetSection("Service:Version").Value}");
        }

        protected override void Poller()
        {
            Console.WriteLine($"Overriden Polling at {DateTime.Now:o}\n");
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