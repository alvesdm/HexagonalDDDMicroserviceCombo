using Domain.Interfaces;
using Domain.Interfaces.Entities;

namespace Domain.Events.Events
{
    public class ItemUpdatedEvent : EventRequestBase<IAmItem, EventResult<int>>
    {
        public ItemUpdatedEvent(IAmItem entity) : base(entity)
        {
        }
    }
}