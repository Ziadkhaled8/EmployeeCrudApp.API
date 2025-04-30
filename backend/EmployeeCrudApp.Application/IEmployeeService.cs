using EmployeeCrudApp.Application.Dtos;

namespace EmployeeCrudApp.Application
{
    public interface IEmployeeService
    {
        Task<GetEmployeeDto?> GetEmployeeById(int id);
        Task<PagedResult<GetEmployeeDto>> GetAllEmployees(int page, int pageSize);
        Task AddEmployee(CreateEmployeeDto employee);
        Task UpdateEmployee(UpdateEmployeeDto employee);
        Task DeleteEmployee(int id);
        Task<PagedResult<GetEmployeeDto>> SearchEmployees(string search, int page, int pageSize);
    }
}
