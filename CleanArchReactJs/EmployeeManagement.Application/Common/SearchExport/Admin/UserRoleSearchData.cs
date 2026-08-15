using EmployeeManagement.Application.DTOs.Admin.UserRoles;
using EmployeeManagement.Domain.Entities.Admin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Common.SearchExport.Admin
{
    public static class UserRoleSearchData
    {
        public async static Task<(List<UserRole> userRoles, int totalRecords)> GetExportUserRoleData(IQueryable<UserRole> query, SearchUserRoleDto dto, string searchFor)
        {
            //-------------------------
            // Keyword Search
            //-------------------------
            List<UserRole> userRoles = new List<UserRole>();

            if (!string.IsNullOrWhiteSpace(dto.Keyword))
            {
                var keyword = dto.Keyword.Trim();

                query = query.Where(x =>
                    EF.Functions.Like(x.Role.Name, $"%{keyword}%") ||
                    EF.Functions.Like(x.User.UserName, $"%{keyword}%"));
            }
            if (dto.RoleId != Guid.Empty)
            {
                query = query.Where(x => x.RoleId == dto.RoleId);
            }
            if (dto.UserId != Guid.Empty)
            {
                query = query.Where(x => x.UserId == dto.UserId);
            }


            //-------------------------
            // Sorting
            //-------------------------

            query = dto.SortBy?.ToLower() switch
            {
                "role" => dto.Descending
                    ? query.OrderByDescending(x => x.Role.Name)
                    : query.OrderBy(x => x.Role.Name),

                "user" => dto.Descending
                    ? query.OrderByDescending(x => x.User.UserName)
                    : query.OrderBy(x => x.User.UserName),

                _ => dto.Descending
                    ? query.OrderByDescending(x => x.Role.Name)
                    : query.OrderBy(x => x.Role.Name)
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
                userRoles = await query
                   .Skip((dto.PageNumber - 1) * dto.PageSize)
                   .Take(dto.PageSize)
                   .ToListAsync();

            }
            else
            {
                userRoles =
                    await query.ToListAsync();

            }


            return (userRoles, totalRecords);
        }

    }
}
