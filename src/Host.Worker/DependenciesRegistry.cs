using Autofac;

namespace Host.Worker
{
    internal class DependenciesRegistry
    {
        public static void Register(ContainerBuilder builder)
        {
            RegisterCars(builder);
            RegisterHouses(builder);
        }

        private static void RegisterHouses(ContainerBuilder builder)
        {
            builder.RegisterType<MyTypeB>();
        }

        private static void RegisterCars(ContainerBuilder builder)
        {
            builder.RegisterType<MyTypeA>();
        }
    }
}