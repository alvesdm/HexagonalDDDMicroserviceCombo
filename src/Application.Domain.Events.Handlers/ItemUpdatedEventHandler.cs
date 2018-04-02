using System.Threading;
using System.Threading.Tasks;
using Domain.Events;
using Domain.Events.Events;
using MediatR;

namespace Application.Domain.Events.Handlers
{
    public class ItemUpdatedEventHandler : IRequestHandler<ItemUpdatedEvent, EventResult<int>>
    {
        public async Task<EventResult<int>> Handle(ItemUpdatedEvent request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new EventResult<int>(request.Entity.Description.Length));
        }
    }
}