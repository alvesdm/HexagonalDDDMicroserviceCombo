using System.Collections.Generic;
using System.Linq;

namespace Domain.Shared.Results
{
    public class SimpleResult<T>
    {
        public SimpleResult(T result)
        {
            Result = result;
            Errors = new List<SimpleResultError>();
        }

        public void AddError(SimpleResultError error)
        {
            Errors.Add(error);
        }

        public void AddError(IEnumerable<SimpleResultError> errors)
        {
            Errors.AddRange(errors);
        }

        public void AddError(string message, string number = "")
        {
            Errors.Add(new SimpleResultError(message, number));
        }

        public T Result { get; set; }
        public bool IsSuccess => !Errors.Any();
        public List<SimpleResultError> Errors { get; set; }
    }
}