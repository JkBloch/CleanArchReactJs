using EmployeeManagement.Application.DTOs.Master.Employee;
using EmployeeManagement.Domain.Entities.Master;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Common.SearchExport.Master
{
    public static class EmployeeSearchData
    {
        public async static Task<(List<Employee> employees, int totalRecords)> GetExportEmployeeData(IQueryable<Employee> query
            , SearchEmployeeDto dto, string searchFor, CancellationToken cancellationToken = default)
        {
            //-------------------------
            // Keyword Search
            //-------------------------
            List<Employee> employees = new List<Employee>();

            if (!string.IsNullOrWhiteSpace(dto.Keyword))
            {
                var keyword = dto.Keyword.Trim();

                query = query.Where(x =>
                    EF.Functions.Like(x.Name, $"%{keyword}%") ||
                    EF.Functions.Like(x.Code, $"%{keyword}%"));
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
            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                var email = dto.Email.Trim();

                query = query.Where(x =>
                EF.Functions.Like(x.Email, $"{email}%"));
            }
            if (dto.DepartmentId != null && dto.DepartmentId != Guid.Empty)
            {
                query = query.Where(x => x.DepartmentId == dto.DepartmentId);
            }
            if (dto.StateId != null && dto.StateId != Guid.Empty)
            {
                query = query.Where(x => x.StateId == dto.StateId);
            }
            if (dto.CityId!= null && dto.CityId != Guid.Empty)
            {
                query = query.Where(x => x.CityId == dto.CityId);
            }
            if (dto.SalaryFrom!= null && dto.SalaryFrom > 0)
            {
                query = query.Where(x => x.Salary >= dto.SalaryFrom);
            }
            if (dto.SalaryTo != null && dto.SalaryTo > 0)
            {
                query = query.Where(x => x.Salary <= dto.SalaryTo);
            }
            if (dto.DateOfBirthFrom != null && dto.DateOfBirthFrom > DateTime.MinValue)
            {
                query = query.Where(x => x.DateOfBirth >= dto.DateOfBirthFrom);
            }
            if (dto.DateOfBirthTo != null && dto.DateOfBirthTo > DateTime.MinValue)
            {
                query = query.Where(x => x.DateOfBirth <= dto.DateOfBirthTo);
            }
            if (dto.JoiningDateFrom != null && dto.JoiningDateFrom > DateTime.MinValue)
            {
                query = query.Where(x => x.JoiningDate >= dto.JoiningDateFrom);
            }
            if (dto.JoiningDateTo != null && dto.JoiningDateTo > DateTime.MinValue)
            {
                query = query.Where(x => x.JoiningDate <= dto.JoiningDateTo);
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
                "email" => dto.Descending 
                    ? query.OrderByDescending(x => x.Email) 
                    : query.OrderBy(x => x.Email),

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
                employees = await query.Include(x=>x.Department).Include(x=>x.State).Include(x=>x.City)
                    .IgnoreQueryFilters()
                   .Skip((dto.PageNumber - 1) * dto.PageSize)
                   .Take(dto.PageSize)
                   .ToListAsync(cancellationToken);

            }
            else
            {
                employees =
                    await query.IgnoreQueryFilters().ToListAsync();

            }


            return (employees, totalRecords);
        }
    }
}
