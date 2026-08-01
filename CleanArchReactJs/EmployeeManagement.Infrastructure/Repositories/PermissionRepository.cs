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
    public class PermissionRepository : GenericRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(AppDbContext context)
           : base(context)
        {
        }
        public async Task<Permission?> GetByNameAsync(string name)
        {
            return await _context.Permissions
                .FirstOrDefaultAsync(x => x.Name == name);
        }
        public async Task<Permission?> GetByCodeAsync(string code)
        {
            return await _context.Permissions
                .FirstOrDefaultAsync(x => x.Code == code);
        }
        public async Task<bool> NameExistsAsync(string name, Guid excludePermissionId)
        {
            return await _context.Permissions
                .AsNoTracking()
                .AnyAsync(x =>
                    x.Name == name &&
                    x.Id != excludePermissionId);
        }
        public async Task<bool> CodeExistsAsync(string permissionCode, Guid excludePermissionId)
        {
            return await _context.Permissions
                .AsNoTracking()
                .AnyAsync(x =>
                    x.Code == permissionCode &&
                    x.Id != excludePermissionId);
        }
        public async Task<Permission?> GetDeletedPermissionAsync(Guid id)
        {
            return await _context.Permissions
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(
                    x => x.Id == id);
        }
        public async Task<IEnumerable<Permission>> SearchAsync(string keyword)
        {
            keyword = keyword.ToLower();

            return await _context.Permissions
                .Where(x =>
                    x.Name.ToLower().Contains(keyword) ||
                    x.Code.ToLower().Contains(keyword))
                .OrderBy(x => x.Name)
                .ToListAsync();
        }
        public IQueryable<Permission> Query()
        {
            return _context.Permissions;

        }
    }
}
