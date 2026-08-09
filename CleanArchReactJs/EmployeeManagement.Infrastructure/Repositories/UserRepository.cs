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
        public async Task<User?> GetByNameAsync(string name)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.UserName == name);
        }     
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email);
        }
        public async Task<bool> NameExistsAsync(string name, Guid excludeUserId)
        {
            return await _context.Users
                .AsNoTracking()
                .AnyAsync(x =>
                    x.UserName == name &&
                    x.Id != excludeUserId);
        }  
        public async Task<bool> EmailExistsAsync(string email, Guid excludeUserId)
        {
            return await _context.Users
                .AsNoTracking()
                .AnyAsync(x =>
                    x.Email == email &&
                    x.Id != excludeUserId);
        }
        public async Task<User?> GetDeletedUserAsync(Guid id)
        {
            return await _context.Users
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(
                    x => x.Id == id);
        }
        public async Task<IEnumerable<User>> SearchAsync(string keyword)
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
                .ToListAsync();
        }
        public IQueryable<User> Query()
        {
            return _context.Users;

        }
    }
}
