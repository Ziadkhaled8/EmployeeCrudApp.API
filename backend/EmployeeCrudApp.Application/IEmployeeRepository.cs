using EmployeeCrudApp.Application.Dtos;
using EmployeeCrudApp.Domain.Entities;

namespace EmployeeCrudApp.Application
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetEmployeeById(int id, bool? asNoTracking = null);
        Task<Employee?> GetEmployeeByEmail(string email, bool? asNoTracking = null);
        Task<PagedResult<Employee>> GetAllEmployees(int page, int pageSize);
        Task AddEmployee(Employee employee);
        Task UpdateEmployee(Employee employee);
        Task DeleteEmployee(Employee employee);
        Task<PagedResult<Employee>> SearchEmployees(string lowerSearch, int page, int pageSize);
    }
}
