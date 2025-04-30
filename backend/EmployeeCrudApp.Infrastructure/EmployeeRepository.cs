using EmployeeCrudApp.Application;
using EmployeeCrudApp.Application.Dtos;
using EmployeeCrudApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCrudApp.Infrastructure
{
    public class EmployeeRepository(ApplicationDBContext context) : IEmployeeRepository
    {
        private readonly DbSet<Employee> _db = context.Set<Employee>();
        private readonly ApplicationDBContext _context = context;

        public async Task<Employee?> GetEmployeeById(int id, bool? asNoTracking=null)
        {
            if (asNoTracking == true)
            {
                return await _db.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            }
            return await _db.FindAsync(id);
        }
        public async Task<Employee?> GetEmployeeByEmail(string email, bool? asNoTracking = null)
        {
            if (asNoTracking == true)
            {
                return await _db.AsNoTracking().FirstOrDefaultAsync(e => e.Email == email);
            }
            return await _db.FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<PagedResult<Employee>> GetAllEmployees(int page, int pageSize)
        {
            var query= _db.AsQueryable();
            var totalCount=await query.CountAsync();
            var result = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PagedResult<Employee>
            {
                TotalCount = totalCount,
                PageSize = pageSize,
                CurrentPage = page,
                Items = result
            };
        }

        public async Task AddEmployee(Employee employee)
        {
            await _db.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployee(Employee employee)
        {
            _db.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployee(Employee employee)
        {

                _db.Remove(employee);
                await _context.SaveChangesAsync();
        }
        public async Task<PagedResult<Employee>> SearchEmployees(string searchTerm, int page, int pageSize)
        {
            var query = _context.Employees
            .Where(e =>
                    e.FirstName.ToLower().Contains(searchTerm) ||
                    e.LastName.ToLower().Contains(searchTerm) ||
                    (e.FirstName + " " + e.LastName).ToLower().Contains(searchTerm)
                ).AsQueryable();
            var totalCount = await query.CountAsync();

            var result=await query
                .Select(e => new
                {
                    e.Id,
                    e.FirstName,
                    e.LastName,
                    e.Position,
                    e.Email,
                    MatchScore =
                        e.FirstName.ToLower() == searchTerm ? 0 :
                        e.FirstName.ToLower().Contains(searchTerm) ? 1 :
                        e.LastName.ToLower() == searchTerm ? 2 :
                        e.LastName.ToLower().Contains(searchTerm) ? 3 :
                        4
                })
                .OrderBy(x => x.MatchScore)
                .ThenBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new Employee
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Position = x.Position,
                    Email = x.Email
                }).ToListAsync();
            return new PagedResult<Employee>
            {
                TotalCount = totalCount,
                PageSize = pageSize,
                CurrentPage = page,
                Items = result
            };

        }
    }
}
