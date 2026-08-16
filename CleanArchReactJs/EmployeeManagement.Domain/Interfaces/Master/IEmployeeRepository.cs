using EmployeeManagement.Domain.Entities.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Interfaces.Master
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<Employee?> GetEmployeeByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Employee?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<Employee?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
        Task<Employee?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<bool> NameExistsAsync(string name, Guid excludeEmployeeId, CancellationToken cancellationToken = default);
        Task<bool> CodeExistsAsync(string employeeCode, Guid excludeEmployeeId, CancellationToken cancellationToken = default);
        Task<bool> EmailExistsAsync(string email, Guid excludeEmployeeId, CancellationToken cancellationToken = default);

        Task<Employee?> GetDeletedEmployeeAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Employee>> SearchAsync(string keyword, CancellationToken cancellationToken = default);
        IQueryable<Employee> Query();
    }
}
