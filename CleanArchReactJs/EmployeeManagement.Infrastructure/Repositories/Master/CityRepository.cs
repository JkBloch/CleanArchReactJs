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
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(AppDbContext context)
           : base(context)
        {
        }
        public async Task<City?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Cities
                .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
        }
        public async Task<City?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return await _context.Cities
                .FirstOrDefaultAsync(x => x.Code == code, cancellationToken);
        }
        public async Task<bool> NameExistsAsync(string name, Guid excludeCityId, CancellationToken cancellationToken = default)
        {
            return await _context.Cities
                .AsNoTracking()
                .AnyAsync(x =>
                    x.Name == name &&
                    x.Id != excludeCityId, cancellationToken);
        }
        public async Task<bool> CodeExistsAsync(string cityCode, Guid excludeCityId, CancellationToken cancellationToken = default)
        {
            return await _context.Cities
                .AsNoTracking()
                .AnyAsync(x =>
                    x.Code == cityCode &&
                    x.Id != excludeCityId, cancellationToken);
        }
        public async Task<City?> GetDeletedCityAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Cities
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(
                    x => x.Id == id
                    , cancellationToken);
        }
        public async Task<IEnumerable<City>> SearchAsync(string keyword, CancellationToken cancellationToken = default)
        {
            keyword = keyword.ToLower();

            return await _context.Cities
                .Where(x =>
                    x.Name.ToLower().Contains(keyword) ||
                    x.Code.ToLower().Contains(keyword))
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }
        public IQueryable<City> Query()
        {
            return _context.Cities;

        }
    }
}
