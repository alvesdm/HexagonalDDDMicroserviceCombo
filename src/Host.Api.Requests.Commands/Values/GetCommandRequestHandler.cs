using System.Threading;
using System.Threading.Tasks;
using Application.Hosts.Ports.Commands;
using Application.Services.Interfaces.Services;
using Domain.Interfaces.Entities;
using Host.Api.Requests.Commands.Bus;
using MediatR;

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
}