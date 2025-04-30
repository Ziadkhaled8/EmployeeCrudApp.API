

namespace EmployeeCrudApp.Domain.Exceptions
{
    public sealed class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string entityName) :
            base($"this {entityName} not found")
        { }
        public NotFoundException(string baseEntity, string childEntity) :
        base($"this {baseEntity} doesn't have any {childEntity}")
        { }
        public NotFoundException(string entityName, string messege, bool nothing = false) :
            base($"this {entityName} not found, and the Identiy Messege are {messege}")
        { }
    }
}
