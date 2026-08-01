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

        //public IEmployeeRepository Employees { get; }

        //public IUserRepository Users { get; }

        //public IRefreshTokenRepository RefreshTokens { get; }

        //public IReportRepository Reports { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Permissions = new PermissionRepository(context);

            //Employees = new EmployeeRepository(context);

            //Users = new UserRepository(context);

            //RefreshTokens = new RefreshTokenRepository(context);
            //Reports = new ReportRepository(context);


        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();

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
