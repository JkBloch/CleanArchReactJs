using EmployeeManagement.Domain.Entities.Master;
using EmployeeManagement.Domain.Interfaces.Master;
using EmployeeManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Infrastructure.Repositories.Master
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context)
           : base(context)
        {
        }

        public async Task<Employee?> GetEmployeeByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .Include(x=>x.Department)
                .Include(x=>x.City)
                .Include(x => x.State)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        public async Task<Employee?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
        }
        public async Task<Employee?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(x => x.Code == code, cancellationToken);
        }
        public async Task<Employee?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(x => x.Email== email, cancellationToken);
        }
        public async Task<bool> NameExistsAsync(string name, Guid excludeEmployeeId, CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .AsNoTracking()
                .AnyAsync(x =>
                    x.Name == name &&
                    x.Id != excludeEmployeeId, cancellationToken);
        }
        public async Task<bool> CodeExistsAsync(string employeeCode, Guid excludeEmployeeId, CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .AsNoTracking()
                .AnyAsync(x =>
                    x.Code == employeeCode &&
                    x.Id != excludeEmployeeId, cancellationToken);
        }
        public async Task<bool> EmailExistsAsync(string email, Guid excludeEmployeeId, CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .AsNoTracking()
                .AnyAsync(x =>
                    x.Email == email &&
                    x.Id != excludeEmployeeId, cancellationToken);
        }
        public async Task<Employee?> GetDeletedEmployeeAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(
                    x => x.Id == id
                    , cancellationToken);
        }
        public async Task<IEnumerable<Employee>> SearchAsync(string keyword, CancellationToken cancellationToken = default)
        {
            keyword = keyword.ToLower();

            return await _context.Employees
                .Where(x =>
                    x.Name.ToLower().Contains(keyword) ||
                    x.Code.ToLower().Contains(keyword))
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }
        public IQueryable<Employee> Query()
        {
            return _context.Employees;

        }
    }

}
