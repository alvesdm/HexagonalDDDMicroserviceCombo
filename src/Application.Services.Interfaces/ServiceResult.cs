using System.Collections.Generic;
using System.Linq;

namespace Application.Services.Interfaces
{
    public class ServiceResult<T>
    {
        public ServiceResult(T result)
        {
            Result = result;
        }

        public void AddError(ServiceResultError error)
        {
            Errors.Add(error);
        }

        public void AddError(string message, int? number = null)
        {
            Errors.Add(new ServiceResultError(message, number));
        }

        public T Result { get; set; }
        public bool IsSuccess => Errors.Any();
        public List<ServiceResultError> Errors { get; set; }
    }
}