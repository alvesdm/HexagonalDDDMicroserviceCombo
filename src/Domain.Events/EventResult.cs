using System.Collections.Generic;
using System.Linq;
using Domain.Shared.Results;

namespace Domain.Events
{
    public class EventResult<T> : SimpleResult<T>
    {
        public EventResult(T result) : base(result)
        {
        }
    }
}