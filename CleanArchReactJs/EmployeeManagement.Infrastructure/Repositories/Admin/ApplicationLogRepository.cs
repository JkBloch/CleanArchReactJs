using EmployeeManagement.Domain.Entities;
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
    public class ApplicationLogRepository : GenericRepository<ApplicationLog>, IApplicationLogRepository
    {
        public ApplicationLogRepository(AppDbContext context)
           : base(context)
        {
        }

        public async Task<ApplicationLog?> GetByLevelAsync(string level)
        {
            return await _context.ApplicationLogs
                .FirstOrDefaultAsync(x => x.Level == level);
        }
       
        public async Task<ApplicationLog?> GetDeletedApplicationLogAsync(Guid id)
        {
            return await _context.ApplicationLogs
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(
                    x => x.Id == id);
        }
        public async Task<IEnumerable<ApplicationLog>> SearchAsync(string keyword)
        {
            keyword = keyword.ToLower();

            return await _context.ApplicationLogs
                .Where(x =>
                    x.Level.ToLower().Contains(keyword) ||
                    x.Message.ToLower().Contains(keyword))
                .OrderBy(x => x.TimeStamp)
                .ToListAsync();
        }
        public IQueryable<ApplicationLog> Query()
        {
            return _context.ApplicationLogs;

        }
    }

}
