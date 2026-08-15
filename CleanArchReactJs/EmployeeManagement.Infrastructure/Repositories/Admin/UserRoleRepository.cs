using EmployeeManagement.Domain.Entities.Admin;
using EmployeeManagement.Domain.Interfaces.Admin;
using EmployeeManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Infrastructure.Repositories.Admin
{
    public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(AppDbContext context)
           : base(context)
        {
        }

        public async Task<UserRole?> GetUserRoleByIdAsync(Guid id)
        {
            return await _context.UserRoles.Include(x => x.Role).Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<bool> UserRoleExistsAsync(Guid roleId, Guid userId, Guid excludeUserRoleId)
        {
            return await _context.UserRoles
                .AsNoTracking()
                .AnyAsync(x =>
                    (x.RoleId == roleId || roleId == Guid.Empty) &&
                    (x.UserId== userId || userId == Guid.Empty) &&
                    x.Id != excludeUserRoleId);
        }

        public async Task<UserRole?> GetDeletedUserRoleAsync(Guid id)
        {
            return await _context.UserRoles
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(
                    x => x.Id == id);
        }

        public IQueryable<UserRole> Query()
        {
            return _context.UserRoles;

        }
    }

}
