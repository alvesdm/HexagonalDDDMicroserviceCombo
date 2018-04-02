using MediatR;

namespace Domain.Events
{
    /// <summary>
    /// Implements an Event without return
    /// Request notification will be handled by ONE handler only
    /// </summary>
    /// <typeparam name="T"> The entity which event is raised</typeparam>
    public interface IAmEventRequest<out T> : IRequest
    {
        T Entity { get; }
    }

    /// <summary>
    /// Implements an Event without return
    /// Request notification will be handled by ONE handler only
    /// </summary>
    /// <typeparam name="T"> The entity which event is raised</typeparam>
    /// <typeparam name="TR">The return type of the event handled</typeparam>
    public interface IAmEventRequest<out T, out TR> : IRequest<TR>
    {
        T Entity { get; }
    }
}