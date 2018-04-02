using System.Reflection;
using Application.Domain.Events.Handlers;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Domain.IoC.Events
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainEvents(this IServiceCollection services)
        {
            services.AddMediatR(
                typeof(IAmEventRequest<>).GetTypeInfo().Assembly, 
                typeof(BaseNotificationHandler<>).GetTypeInfo().Assembly);
            return services;
        }
    }
}
