

namespace EmployeeCrudApp.Domain.Exceptions
{
    public class BadRequestException:Exception
    {
        public BadRequestException()
        {
        }
        public BadRequestException(string message) : base(message)
        {
        }
        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public BadRequestException(string entityName, string message) : base($"this {entityName} already exists, and the Identiy Messege are {message}")
        {
        }
    }
}
