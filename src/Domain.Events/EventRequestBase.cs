namespace Domain.Events
{
    public abstract class EventRequestBase<T> : IAmEventRequest<T>
    {
        protected EventRequestBase(T entity)
        {
            Entity = entity;
        }

        public T Entity { get; }
    }

    public abstract class EventRequestBase<T, TR> : IAmEventRequest<T, TR>
    {
        protected EventRequestBase(T entity)
        {
            Entity = entity;
        }

        public T Entity { get; }
    }
}