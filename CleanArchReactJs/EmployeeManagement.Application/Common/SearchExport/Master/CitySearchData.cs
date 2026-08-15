using EmployeeManagement.Application.DTOs.Master.City;
using EmployeeManagement.Domain.Entities.Master;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Common.SearchExport.Master
{
    public static class CitySearchData
    {
        public async static Task<(List<City> cities, int totalRecords)> GetExportCityData(IQueryable<City> query, 
            SearchCityDto dto, string searchFor, CancellationToken cancellationToken = default)
        {
            //-------------------------
            // Keyword Search
            //-------------------------
            List<City> cities = new List<City>();

            if (!string.IsNullOrWhiteSpace(dto.Keyword))
            {
                var keyword = dto.Keyword.Trim();

                query = query.Where(x =>
                    EF.Functions.Like(x.Name, $"%{keyword}%") ||
                    EF.Functions.Like(x.Code, $"%{keyword}%"));
            }
            if (dto.StateId != null && dto.StateId != Guid.Empty)
            {
                query = query.Where(x => x.StateId == dto.StateId);
            }
            if (!string.IsNullOrWhiteSpace(dto.Code))
            {
                var code = dto.Code.Trim();

                query = query.Where(x =>
                EF.Functions.Like(x.Code, $"{code}%"));
            }
            if (!string.IsNullOrWhiteSpace(dto.Name))
            {
                var name = dto.Name.Trim();

                query = query.Where(x =>
                EF.Functions.Like(x.Name, $"{name}%"));
            }


            //-------------------------
            // Sorting
            //-------------------------

            query = dto.SortBy?.ToLower() switch
            {
                "code" => dto.Descending
                    ? query.OrderByDescending(x => x.Code)
                    : query.OrderBy(x => x.Code),

                "name" => dto.Descending
                    ? query.OrderByDescending(x => x.Name)
                    : query.OrderBy(x => x.Name),

                _ => dto.Descending
                    ? query.OrderByDescending(x => x.Name)
                    : query.OrderBy(x => x.Name)
            };

            //-------------------------
            // Count
            //-------------------------

            int totalRecords = await query.CountAsync();

            //-------------------------
            // Paging
            //-------------------------

            dto.PageSize = Math.Min(dto.PageSize, 100);

            if (searchFor == "page")
            {
                cities = await query
                    .IgnoreQueryFilters()
                   .Skip((dto.PageNumber - 1) * dto.PageSize)
                   .Take(dto.PageSize)
                   .ToListAsync(cancellationToken);

            }
            else
            {
                cities =
                    await query.IgnoreQueryFilters().ToListAsync(cancellationToken);

            }


            return (cities, totalRecords);
        }

    }
}
