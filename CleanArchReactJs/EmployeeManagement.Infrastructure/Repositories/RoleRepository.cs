using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Infrastructure.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext context)
           : base(context)
        {
        }
        public async Task<Role?> GetByNameAsync(string name)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(x => x.Name == name);
        }
        public async Task<Role?> GetByCodeAsync(string code)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(x => x.Code == code);
        }
        public async Task<bool> NameExistsAsync(string name, Guid excludeRoleId)
        {
            return await _context.Roles
                .AsNoTracking()
                .AnyAsync(x =>
                    x.Name == name &&
                    x.Id != excludeRoleId);
        }
        public async Task<bool> CodeExistsAsync(string roleCode, Guid excludeRoleId)
        {
            return await _context.Roles
                .AsNoTracking()
                .AnyAsync(x =>
                    x.Code == roleCode &&
                    x.Id != excludeRoleId);
        }
        public async Task<Role?> GetDeletedRoleAsync(Guid id)
        {
            return await _context.Roles
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(
                    x => x.Id == id);
        }
        public async Task<IEnumerable<Role>> SearchAsync(string keyword)
        {
            keyword = keyword.ToLower();

            return await _context.Roles
                .Where(x =>
                    x.Name.ToLower().Contains(keyword) ||
                    x.Code.ToLower().Contains(keyword))
                .OrderBy(x => x.Name)
                .ToListAsync();
        }
        public IQueryable<Role> Query()
        {
            return _context.Roles;

        }
    }
}
