using EmployeeManagement.Domain.Interfaces.Admin;
using EmployeeManagement.Domain.Interfaces.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        IRefreshTokenRepository RefreshTokens { get; }
        IStateRepository States { get; }
        ICityRepository Cities { get; }

        //IEmployeeRepository Employees { get; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
