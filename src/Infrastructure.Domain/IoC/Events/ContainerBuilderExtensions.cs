using System.Collections.Generic;
using System.Reflection;
using Application.Domain.Events.Handlers;
using Autofac;
using Domain.Events;
using MediatR;

namespace Infrastructure.Domain.IoC.Events
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterDomainEvents(this ContainerBuilder builder)
        {
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            // request handlers
            builder
                .Register<SingleInstanceFactory>(ctx => {
                    var c = ctx.Resolve<IComponentContext>();
                    return t => c.TryResolve(t, out var o) ? o : null;
                })
                .InstancePerLifetimeScope();

            // notification handlers
            builder
                .Register<MultiInstanceFactory>(ctx => {
                    var c = ctx.Resolve<IComponentContext>();
                    return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
                })
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(IAmEventRequest<>).GetTypeInfo().Assembly)
                .Where(t => typeof(IAmEventRequest<>).IsAssignableFrom(t))
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(BaseNotificationHandler<>).GetTypeInfo().Assembly)
                .Where(t => typeof(BaseNotificationHandler<>).IsAssignableFrom(t))
                .AsImplementedInterfaces();
            return builder;
        }
    }
}
