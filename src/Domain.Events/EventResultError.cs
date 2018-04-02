using Domain.Shared.Results;

namespace Domain.Events
{
    public class EventResultError : SimpleResultError
    {
        public EventResultError(string message, string errorCode = "") : base(message, errorCode)
        {
        }
    }
}