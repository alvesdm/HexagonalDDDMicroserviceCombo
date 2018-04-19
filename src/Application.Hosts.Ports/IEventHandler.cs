namespace Application.Hosts.Ports
{
    public interface IEventHandler<TEvent> : IMessageHandler<TEvent>
        where TEvent : IEvent
    {
    }
}