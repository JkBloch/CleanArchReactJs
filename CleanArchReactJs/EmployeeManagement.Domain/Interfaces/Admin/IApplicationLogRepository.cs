using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Interfaces.Admin
{
    public interface IApplicationLogRepository : IGenericRepository<ApplicationLog>
    {
        Task<ApplicationLog?> GetByLevelAsync(string level);
        Task<ApplicationLog?> GetDeletedApplicationLogAsync(Guid id);
        Task<IEnumerable<ApplicationLog>> SearchAsync(string keyword);
        IQueryable<ApplicationLog> Query();
    }
}
