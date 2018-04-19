using System;
using Host.Worker.Core;

namespace Host.Worker
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Bootstrap<Service>
                .Instance
                .WithDependencies(DependenciesRegistry.Register)
                .WithMessageHandlers(MessageHandlersRegistry.Register)
                .WithName(()=>"BillingService")
                .Run();
        }
    }
}
