using System.Threading;
using System.Threading.Tasks;
using Application.Hosts.Ports.Commands;
using Application.Services.Interfaces.Services;
using Domain.Interfaces.Entities;
using MediatR;
using RabbitHole;

namespace Host.Api.Requests.Commands.Values
{
    public class GetCommandRequestHandler : IAmApiCommandHandler, IRequestHandler<GetCommandRequest, IAmItem>
    {
        private readonly IAmItemService _itemService;
        private readonly IAmTodoBus _bus;

        public GetCommandRequestHandler(IAmItemService itemService, IAmTodoBus bus)
        {
            _itemService = itemService;
            _bus = bus;
        }

        public async Task<IAmItem> Handle(GetCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _itemService.Add(request.Item);

            _bus.Publish(new AddTaskCommand("ha!!"));

            return result.Result;
        }
    }

    public interface IAmApiBus : IBus { }

    public interface IAmTodoBus : IAmApiBus
    {
        void Publish(AddTaskCommand message);
    }

    public class TodoBus : BusBase, IAmTodoBus
    {
        public TodoBus(IClient client) : base(client)
        {
        }

        public void Publish(AddTaskCommand message)
        {
            Client
                .DeclareExchange(e => e.WithName("Service1").BeingDurable(false))
                .Publish<AddTaskCommand>(
                    m =>
                        m.WithExchange("Service1")
                            .WithMessage(message));
        }
    }
}