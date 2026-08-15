using EmployeeManagement.Domain.Entities.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Interfaces.Master
{
    public interface ICityRepository : IGenericRepository<City>
    {
        Task<City?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<City?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
        Task<bool> NameExistsAsync(string name, Guid excludeCityId, CancellationToken cancellationToken = default);
        Task<bool> CodeExistsAsync(string cityCode, Guid excludeCityId, CancellationToken cancellationToken = default);
        Task<City?> GetDeletedCityAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<City>> SearchAsync(string keyword, CancellationToken cancellationToken = default);
        IQueryable<City> Query();
    }
}
