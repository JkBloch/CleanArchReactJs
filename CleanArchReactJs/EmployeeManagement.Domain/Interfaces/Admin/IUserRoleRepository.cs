using EmployeeManagement.Domain.Entities.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Interfaces.Admin
{
    public interface IUserRoleRepository : IGenericRepository<UserRole>
    {
        Task<UserRole?> GetUserRoleByIdAsync(Guid id);
        Task<bool> UserRoleExistsAsync(Guid roleId, Guid userId, Guid excludeUserRoleId);
        Task<UserRole?> GetDeletedUserRoleAsync(Guid id);
        IQueryable<UserRole> Query();
    }
}
