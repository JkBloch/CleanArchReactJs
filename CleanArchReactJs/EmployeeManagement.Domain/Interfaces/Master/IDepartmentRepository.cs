using EmployeeManagement.Domain.Entities.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Interfaces.Master
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task<Department?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<Department?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
        Task<bool> NameExistsAsync(string name, Guid excludeDepartmentId, CancellationToken cancellationToken = default);
        Task<bool> CodeExistsAsync(string departmentCode, Guid excludeDepartmentId, CancellationToken cancellationToken = default);
        Task<Department?> GetDeletedDepartmentAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Department>> SearchAsync(string keyword, CancellationToken cancellationToken = default);
        IQueryable<Department> Query();
    }
}
