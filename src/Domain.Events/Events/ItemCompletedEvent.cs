using Domain.Interfaces.Entities;

namespace Domain.Events.Events
{
    public class ItemCompletedEvent : EventRequestBase<IAmItem>
    {
        public ItemCompletedEvent(IAmItem entity) : base(entity)
        {
        }
    }
}
