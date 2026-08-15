using EmployeeManagement.Domain.Entities.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Interfaces.Admin
{
    public interface IRolePermissionRepository : IGenericRepository<RolePermission>
    {
        Task<RolePermission?> GetRolePermissionByIdAsync(Guid id);
        Task<bool> RolePermissionExistsAsync(Guid roleId, Guid permissionId , Guid excludeRolePermissionId);
        Task<RolePermission?> GetDeletedRolePermissionAsync(Guid id);
        IQueryable<RolePermission> Query();
    }
}
