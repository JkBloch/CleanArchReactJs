using EmployeeManagement.Domain.Entities.Master;
using EmployeeManagement.Domain.Interfaces.Master;
using EmployeeManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Infrastructure.Repositories.Master
{
    public class StateRepository : GenericRepository<State>, IStateRepository
    {
        public StateRepository(AppDbContext context)
           : base(context)
        {
        }
        public async Task<State?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.States
                .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
        }
        public async Task<State?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return await _context.States
                .FirstOrDefaultAsync(x => x.Code == code, cancellationToken);
        }
        public async Task<bool> NameExistsAsync(string name, Guid excludeStateId, CancellationToken cancellationToken = default)
        {
            return await _context.States
                .AsNoTracking()
                .AnyAsync(x =>
                    x.Name == name &&
                    x.Id != excludeStateId, cancellationToken);
        }
        public async Task<bool> CodeExistsAsync(string stateCode, Guid excludeStateId   , CancellationToken cancellationToken = default)
        {
            return await _context.States
                .AsNoTracking()
                .AnyAsync(x =>
                    x.Code == stateCode &&
                    x.Id != excludeStateId, cancellationToken);
        }
        public async Task<State?> GetDeletedStateAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.States
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(
                    x => x.Id == id
                    ,cancellationToken);
        }
        public async Task<IEnumerable<State>> SearchAsync(string keyword, CancellationToken cancellationToken = default)
        {
            keyword = keyword.ToLower();

            return await _context.States
                .Where(x =>
                    x.Name.ToLower().Contains(keyword) ||
                    x.Code.ToLower().Contains(keyword))
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }
        public IQueryable<State> Query()
        {
            return _context.States;

        }
    }

}
