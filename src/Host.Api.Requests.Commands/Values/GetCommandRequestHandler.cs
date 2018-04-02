using System.Threading;
using System.Threading.Tasks;
using Application.Services.Interfaces.Services;
using Domain.Interfaces.Entities;
using MediatR;

namespace Host.Api.Requests.Commands.Values
{
    public class GetCommandRequestHandler : IAmApiCommandHandler, IRequestHandler<GetCommandRequest, IAmItem>
    {
        private readonly IAmItemService _itemService;

        public GetCommandRequestHandler(IAmItemService itemService)
        {
            _itemService = itemService;
        }

        public async Task<IAmItem> Handle(GetCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _itemService.Add(request.Item);

            return result.Result;
        }
    }
}