using MediatR;

namespace Domain.Events
{
    /// <summary>
    /// Implement a notification event
    /// Notification events can be handled by 0, 1 or many habdlers
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAmEventNotification<out T> : INotification
    {
        T Entity { get; }
    }
}