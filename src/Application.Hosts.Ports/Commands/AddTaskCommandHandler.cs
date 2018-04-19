using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Hosts.Ports.Commands
{
    public class AddTaskCommandHandler : ICommandHandler<AddTaskCommand, bool>
    {
        public Task Handle(AddTaskCommand message)
        {
            //throw new NotImplementedException();
            return Task.FromResult(1);
        }
    }
}