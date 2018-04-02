using Domain.Interfaces.Entities;
using MediatR;

namespace Host.Api.Requests.Commands.Values
{
    public class GetCommandRequest : IAmApiCommand, IRequest<IAmItem>
    {
        public IAmItem Item { get; }

        public GetCommandRequest(IAmItem item)
        {
            Item = item;
        }
    }
}
