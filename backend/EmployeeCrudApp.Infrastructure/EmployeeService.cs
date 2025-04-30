using AutoMapper;
using EmployeeCrudApp.Application;
using EmployeeCrudApp.Application.Dtos;
using EmployeeCrudApp.Domain.Entities;
using EmployeeCrudApp.Domain.Exceptions;

namespace EmployeeCrudApp.Infrastructure
{
    public class EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper) : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<GetEmployeeDto?> GetEmployeeById(int id)
        {
            var emp = await _employeeRepository.GetEmployeeById(id)
                ?? throw new NotFoundException($"employee with the id {id}");
            return _mapper.Map<GetEmployeeDto>(emp);
        }
        public async Task<PagedResult<GetEmployeeDto>> GetAllEmployees(int page, int pageSize)
        {
            var emps = await _employeeRepository.GetAllEmployees(page, pageSize);
            var mappedItems = _mapper.Map<List<GetEmployeeDto>>(emps.Items);
            return new PagedResult<GetEmployeeDto>
            {
                Items = mappedItems,
                TotalCount = emps.TotalCount,
                PageSize = emps.PageSize,
                CurrentPage = emps.CurrentPage
            };
        }
        public async Task AddEmployee(CreateEmployeeDto employee)
        {
            var emp = await _employeeRepository.GetEmployeeByEmail(employee.Email);
            if (emp != null)
            {
                throw new BadRequestException($"Employee with the email {employee.Email} already exists");
            }
            var mappedEmployee = _mapper.Map<Employee>(employee);
            await _employeeRepository.AddEmployee(mappedEmployee);
        }
        public async Task UpdateEmployee(UpdateEmployeeDto employee)
        {
            var emp = await _employeeRepository.GetEmployeeById(employee.Id,true)
                ?? throw new NotFoundException($"employee with the id {employee.Id}");
            var emailExists = await _employeeRepository.GetEmployeeByEmail(employee.Email, true);
            if (emp != null && emailExists!.Id!=employee.Id)
            {
                throw new BadRequestException($"Employee with the email {employee.Email} already exists");
            }
            var mappedEmployee = _mapper.Map<Employee>(employee);
            await _employeeRepository.UpdateEmployee(mappedEmployee);
        }
        public async Task DeleteEmployee(int id)
        {
            var emp = await _employeeRepository.GetEmployeeById(id)
                ?? throw new NotFoundException($"employee with the id {id}");
            await _employeeRepository.DeleteEmployee(emp);
        }
        public async Task<PagedResult<GetEmployeeDto>> SearchEmployees(string search, int page, int pageSize)
        {
            var lowerSearch = search.ToLower();
            var emps = await _employeeRepository.SearchEmployees(lowerSearch, page, pageSize);
            var mappedItems = _mapper.Map<List<GetEmployeeDto>>(emps.Items);
            return new PagedResult<GetEmployeeDto>
            {
                Items = mappedItems,
                TotalCount = emps.TotalCount,
                PageSize = emps.PageSize,
                CurrentPage = emps.CurrentPage
            };
        }
    }
}
