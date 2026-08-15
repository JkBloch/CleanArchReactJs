using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Infrastructure.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IPermissionRepository Permissions { get; }
        public IRoleRepository Roles { get; }
        public IRolePermissionRepository RolePermissions { get; }
        public IUserRepository Users { get; }
        public IUserRoleRepository UserRoles { get; }
        public IRefreshTokenRepository RefreshTokens { get; }
        public IStateRepository States { get; }

        //public IEmployeeRepository Employees { get; }


        //public IReportRepository Reports { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Permissions = new PermissionRepository(context);
            Roles =new RoleRepository(context);
            RolePermissions = new RolePermissionRepository(context);
            Users = new UserRepository(context);
            UserRoles = new UserRoleRepository(context);
            RefreshTokens = new RefreshTokenRepository(context);
            States=new StateRepository(context);
            //Employees = new EmployeeRepository(context);


            //Reports = new ReportRepository(context);


        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _context.SaveChangesAsync(cancellationToken);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
