using System.Threading;
using System.Threading.Tasks;
using Domain.Events.Events;
using MediatR;

namespace Application.Domain.Events.Handlers
{
    public class ItemCompletedEventHandler : IRequestHandler<ItemCompletedEvent>
    {
        public async Task Handle(ItemCompletedEvent request, CancellationToken cancellationToken)
        {
            await Task.FromResult(0);
        }
    }
}
