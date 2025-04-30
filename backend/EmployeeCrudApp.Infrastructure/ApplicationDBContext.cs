using EmployeeCrudApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCrudApp.Infrastructure
{
    public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : DbContext(options)
    {
        public DbSet<Employee> Employees { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<Employee>().HasIndex(e=>e.Email).IsUnique();
        }
    }
   
}
