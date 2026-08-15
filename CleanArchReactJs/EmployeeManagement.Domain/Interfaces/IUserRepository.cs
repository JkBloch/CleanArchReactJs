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
        Task<User?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<bool> NameExistsAsync(string name, Guid excludeUserId, CancellationToken cancellationToken = default);
        Task<bool> EmailExistsAsync(string userCode, Guid excludeUserId, CancellationToken cancellationToken = default);
        Task<User?> GetDeletedUserAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<User>> SearchAsync(string keyword, CancellationToken cancellationToken = default);
        IQueryable<User> Query();
    }
}
