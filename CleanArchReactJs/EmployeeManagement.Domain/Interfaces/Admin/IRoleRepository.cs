using EmployeeManagement.Domain.Entities.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Interfaces.Admin
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role?> GetByNameAsync(string name);
        Task<Role?> GetByCodeAsync(string code);
        Task<bool> NameExistsAsync(string name, Guid excludeRoleId);
        Task<bool> CodeExistsAsync(string roleCode, Guid excludeRoleId);
        Task<Role?> GetDeletedRoleAsync(Guid id);
        Task<IEnumerable<Role>> SearchAsync(string keyword);
        IQueryable<Role> Query();
    }
}
