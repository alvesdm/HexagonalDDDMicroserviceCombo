using System;
using System.Collections.Generic;
using System.Text;
using Domain.Shared.Results;

namespace Domain.Shared
{
    public class DomainValidationException : Exception
    {
        public DomainValidationException(object entity, List<SimpleResultError> errors)
        {
            Errors = errors;
            Message = $"[{entity.GetType().Name}] has some validation errors.";
        }

        public override string Message { get; }
        public List<SimpleResultError> Errors { get; }
    }
}
