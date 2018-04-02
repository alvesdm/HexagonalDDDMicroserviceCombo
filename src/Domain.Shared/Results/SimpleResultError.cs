using System;

namespace Domain.Shared.Results
{
    public class SimpleResultError
    {
        public SimpleResultError(string message, string errorCode = "")
        {
            Message = message;
            ErrorCode = errorCode;
        }

        public string Message { get; }
        public string ErrorCode { get; }
    }
}