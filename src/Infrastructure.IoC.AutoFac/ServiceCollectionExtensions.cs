using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC.AutoFac
{
    public static class ServiceCollectionExtensions
    {
        public static AutofacServiceProvider AddAutoFac(
            this IServiceCollection services, 
            IContainer applicationContainer, 
            Action<ContainerBuilder> registerTypesAction)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            registerTypesAction(builder);
            applicationContainer = builder.Build();

            return new AutofacServiceProvider(applicationContainer);
        }
    }
}
