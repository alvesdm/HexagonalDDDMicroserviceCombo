using System.Reflection;
using Autofac;

namespace Host.Api.Requests.Commands
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterApiCommands(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IAmApiCommand).GetTypeInfo().Assembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(IAmApiCommandHandler).GetTypeInfo().Assembly).AsImplementedInterfaces();
            return builder;
        }
    }
}
