using System.Reflection;
using Autofac;
using Domain.Validators;
using FluentValidation;

namespace Infrastructure.Domain.IoC.Validatiors
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterDomainValidators(this ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(IAmDomainValidator).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(AbstractValidator<>)).AsImplementedInterfaces();

            return builder;
        }
    }
}
