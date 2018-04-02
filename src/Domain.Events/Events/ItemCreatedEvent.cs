using Domain.Interfaces;
using Domain.Interfaces.Entities;

namespace Domain.Events.Events
{
    public class ItemCreatedEvent : EventNotificationBase<IAmItem>
    {
        public ItemCreatedEvent(IAmItem entity) : base(entity)
        {
        }
    }
}