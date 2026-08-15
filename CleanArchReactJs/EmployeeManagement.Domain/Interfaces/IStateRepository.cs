using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Interfaces
{
    public interface IStateRepository : IGenericRepository<State>
    {
        Task<State?> GetByNameAsync(string name,CancellationToken cancellationToken=default);
        Task<State?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
        Task<bool> NameExistsAsync(string name, Guid excludeStateId, CancellationToken cancellationToken = default);
        Task<bool> CodeExistsAsync(string stateCode, Guid excludeStateId, CancellationToken cancellationToken = default);
        Task<State?> GetDeletedStateAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<State>> SearchAsync(string keyword, CancellationToken cancellationToken = default);
        IQueryable<State> Query();
    }
}
