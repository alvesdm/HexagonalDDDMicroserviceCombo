using Application.Services;
using Application.Services.Interfaces.Services;
using Autofac;
using Domain.Interfaces.Repositories;
using Infrastructure.Domain.Data;

namespace Host.Api.IoC
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterRepositories(this ContainerBuilder builder)
        {
            builder.RegisterType<ItemRepository>().As<IAmItemRepository>().InstancePerLifetimeScope();
            return builder;
        }

        public static ContainerBuilder RegisterServices(this ContainerBuilder builder)
        {
            builder.RegisterType<ItemService>().As<IAmItemService>().InstancePerLifetimeScope();
            return builder;
        }
    }
}
