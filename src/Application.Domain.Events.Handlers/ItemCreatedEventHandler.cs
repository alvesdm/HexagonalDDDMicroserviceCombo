using Domain.Events.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Domain.Events.Handlers
{
    public class ItemCreatedEventHandler : BaseNotificationHandler<ItemCreatedEvent>
    {
        public override async Task Handle(ItemCreatedEvent notification, CancellationToken cancellationToken)
        {
            await Task.FromResult(0);
        }
    }
}