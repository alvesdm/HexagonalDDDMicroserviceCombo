using Application.Hosts.Ports.Commands;

namespace Host.Api.Requests.Commands.Bus
{
    public interface IAmTodoBus : IAmApiBus
    {
        void Publish(AddTaskCommand message);
    }
}