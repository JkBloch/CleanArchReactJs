using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Interfaces
{
    public interface IPermissionRepository : IGenericRepository<Permission>
    {
        Task<Permission?> GetByNameAsync(string name);
        Task<Permission?> GetByCodeAsync(string code);
        Task<bool> NameExistsAsync(string name, Guid excludePermissionId);
        Task<bool> CodeExistsAsync(string permissionCode, Guid excludePermissionId);
        Task<Permission?> GetDeletedPermissionAsync(Guid id);
        Task<IEnumerable<Permission>> SearchAsync(string keyword);
        IQueryable<Permission> Query();
    }
}
