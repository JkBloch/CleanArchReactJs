using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Interfaces
{
   

    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByNameAsync(string name);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> NameExistsAsync(string name, Guid excludeUserId);
        Task<bool> EmailExistsAsync(string userCode, Guid excludeUserId);
        Task<User?> GetDeletedUserAsync(Guid id);
        Task<IEnumerable<User>> SearchAsync(string keyword);
        IQueryable<User> Query();
    }
}
