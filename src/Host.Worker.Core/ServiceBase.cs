using System;
using Autofac;
using PeterKottas.DotNetCore.WindowsService.Base;
using PeterKottas.DotNetCore.WindowsService.Interfaces;

namespace Host.Worker.Core
{
    public abstract class ServiceBase : MicroService, IMicroService, IHaveContainer
    {
        public IContainer Container { get; set; }
        protected int PollerInterval = 1000;

        public void Start()
        {
            StartBase();
            StartPoller();
            Console.WriteLine("I started");
        }

        private void StartPoller()
        {
            if (PollerInterval > 0)
            {
                Timers.Start("Poller", PollerInterval, Poller,
                    e =>
                    {
                        Console.WriteLine($"Exception while polling: {e}\n");
                    });
            }
        }

        protected virtual void Poller()
        {
            Console.WriteLine($"Polling at {DateTime.Now:o}\n");
        }

        public void Stop()
        {
            StopBase();
            Console.WriteLine("I stopped");
        }
    }
}