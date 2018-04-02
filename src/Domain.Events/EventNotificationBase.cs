namespace Domain.Events
{
    public abstract class EventNotificationBase<T> : IAmEventNotification<T>
    {
        protected EventNotificationBase(T entity)
        {
            Entity = entity;
        }

        public T Entity { get; }
    }
}