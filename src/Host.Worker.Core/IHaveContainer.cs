using Autofac;

namespace Host.Worker.Core
{
    public interface IHaveContainer
    {
        IContainer Container { get; set; }
    }
}