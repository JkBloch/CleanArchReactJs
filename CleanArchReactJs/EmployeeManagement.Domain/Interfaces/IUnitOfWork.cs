using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPermissionRepository Permissions { get; }
        IRoleRepository Roles { get; }
        IRolePermissionRepository RolePermissions { get; }
        IUserRepository Users  { get; }
        IUserRoleRepository UserRoles { get; }

        //IEmployeeRepository Employees { get; }

        //IRefreshTokenRepository RefreshTokens { get; }

        Task<int> SaveChangesAsync();
    }
}
