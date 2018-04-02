namespace Application.Services.Interfaces
{
    public class ServiceResultError
    {
        public ServiceResultError(string message, int? number = null)
        {
            Message = message;
            ErrorNumber = number;
        }

        public string Message { get; }
        public int? ErrorNumber { get; }
    }
}