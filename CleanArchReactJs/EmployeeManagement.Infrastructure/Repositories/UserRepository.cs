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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context)
           : base(context)
        {
        }
        public async Task<User?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Users.Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .ThenInclude(x => x.RolePermissions)
                .ThenInclude(x=>x.Permission)
                .FirstOrDefaultAsync(x => x.UserName == name, cancellationToken);
        }     
        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Users.Include(x=>x.UserRoles)
                .ThenInclude(x=>x.Role)
                .ThenInclude(x=>x.RolePermissions)
                .ThenInclude(x=>x.Permission)
                .FirstOrDefaultAsync(x => x.Email == email,cancellationToken);
        }
        public async Task<bool> NameExistsAsync(string name, Guid excludeUserId, CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .AsNoTracking()
                .AnyAsync(x =>
                    x.UserName == name &&
                    x.Id != excludeUserId, cancellationToken);
        }  
        public async Task<bool> EmailExistsAsync(string email, Guid excludeUserId, CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .AsNoTracking()
                .AnyAsync(x =>
                    x.Email == email &&
                    x.Id != excludeUserId, cancellationToken);
        }
        public async Task<User?> GetDeletedUserAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(
                    x => x.Id == id, cancellationToken);
        }
        public async Task<IEnumerable<User>> SearchAsync(string keyword, CancellationToken cancellationToken = default)
        {
            keyword = keyword.ToLower();

            return await _context.Users
                .Where(x =>
                    x.UserName.ToLower().Contains(keyword) ||
                    x.FirstName.ToLower().Contains(keyword) ||
                    x.LastName.ToLower().Contains(keyword) ||
                    x.Email.ToLower().Contains(keyword) 
                    )
                .OrderBy(x => x.UserName)
                .ToListAsync(cancellationToken);
        }
        public IQueryable<User> Query()
        {
            return _context.Users;

        }
    }
}
