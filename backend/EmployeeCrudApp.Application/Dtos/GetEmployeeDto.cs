

namespace EmployeeCrudApp.Application.Dtos
{
    public class GetEmployeeDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Position { get; set; } = null!;

        public string Email { get; set; } = null!;
    }

}
