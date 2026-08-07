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
    public class RolePermissionRepository : GenericRepository<RolePermission>, IRolePermissionRepository
    {
        public RolePermissionRepository(AppDbContext context)
           : base(context)
        {
        }


        public async Task<bool> RolePermissionExistsAsync(Guid roleId, Guid permissionId, Guid excludeRolePermissionId)
        {
            return await _context.RolePermissions
                .AsNoTracking()
                .AnyAsync(x =>
                    (x.RoleId == roleId || roleId==Guid.Empty) &&
                    (x.PermissionId == permissionId || permissionId == Guid.Empty) &&
                    x.Id != excludeRolePermissionId);
        }
      
        public async Task<RolePermission?> GetDeletedRolePermissionAsync(Guid id)
        {
            return await _context.RolePermissions
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(
                    x => x.Id == id);
        }
       
        public IQueryable<RolePermission> Query()
        {
            return _context.RolePermissions;

        }
    }
}
