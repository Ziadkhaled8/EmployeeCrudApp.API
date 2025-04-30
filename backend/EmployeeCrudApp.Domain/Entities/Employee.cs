using System.ComponentModel.DataAnnotations;

namespace EmployeeCrudApp.Domain.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(1,30)]
        public string FirstName { get; set; } = null!;
        [Required]
        [Range(1, 30)]
        public string LastName { get; set; } = null!;
        [Required]
        [Range(1, 30)]
        public string Position { get; set; }= null!;
        [Required, EmailAddress]
        public string Email { get; set; }=null!;

    }
}
