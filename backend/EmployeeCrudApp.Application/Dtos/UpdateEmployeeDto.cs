using System.ComponentModel.DataAnnotations;

namespace EmployeeCrudApp.Application.Dtos
{
    public class UpdateEmployeeDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string Position { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }

}
