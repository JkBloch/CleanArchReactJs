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
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext context)
           : base(context)
        {
        }
        public async Task<Department?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Departments
                .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
        }
        public async Task<Department?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return await _context.Departments
                .FirstOrDefaultAsync(x => x.Code == code, cancellationToken);
        }
        public async Task<bool> NameExistsAsync(string name, Guid excludeDepartmentId, CancellationToken cancellationToken = default)
        {
            return await _context.Departments
                .AsNoTracking()
                .AnyAsync(x =>
                    x.Name == name &&
                    x.Id != excludeDepartmentId, cancellationToken);
        }
        public async Task<bool> CodeExistsAsync(string departmentCode, Guid excludeDepartmentId, CancellationToken cancellationToken = default)
        {
            return await _context.Departments
                .AsNoTracking()
                .AnyAsync(x =>
                    x.Code == departmentCode &&
                    x.Id != excludeDepartmentId, cancellationToken);
        }
        public async Task<Department?> GetDeletedDepartmentAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Departments
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(
                    x => x.Id == id
                    , cancellationToken);
        }
        public async Task<IEnumerable<Department>> SearchAsync(string keyword, CancellationToken cancellationToken = default)
        {
            keyword = keyword.ToLower();

            return await _context.Departments
                .Where(x =>
                    x.Name.ToLower().Contains(keyword) ||
                    x.Code.ToLower().Contains(keyword))
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }
        public IQueryable<Department> Query()
        {
            return _context.Departments;

        }
    }

}
