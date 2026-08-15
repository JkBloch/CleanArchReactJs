using EmployeeManagement.Application.DTOs.Admin.RolePermissions;
using EmployeeManagement.Domain.Entities.Admin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Common.SearchExport.Admin
{
    public static class RolePermissionSearchData
    {
        public async static Task<(List<RolePermission> rolePermissions, int totalRecords)> GetExportRolePermissionData(IQueryable<RolePermission> query, SearchRolePermissionDto dto, string searchFor)
        {
            //-------------------------
            // Keyword Search
            //-------------------------
            List<RolePermission> rolePermissions = new List<RolePermission>();

            if (!string.IsNullOrWhiteSpace(dto.Keyword))
            {
                var keyword = dto.Keyword.Trim();

                query = query.Where(x =>
                    EF.Functions.Like(x.Role.Name, $"%{keyword}%") ||
                    EF.Functions.Like(x.Permission.Name, $"%{keyword}%"));
            }
            if (dto.RoleId != Guid.Empty)
            {
                query = query.Where(x => x.RoleId == dto.RoleId);
            }
            if (dto.PermissionId != Guid.Empty)
            {
                query = query.Where(x => x.PermissionId == dto.PermissionId);
            }


            //-------------------------
            // Sorting
            //-------------------------

            query = dto.SortBy?.ToLower() switch
            {
                "role" => dto.Descending
                    ? query.OrderByDescending(x => x.Role.Name)
                    : query.OrderBy(x => x.Role.Name),

                "permission" => dto.Descending
                    ? query.OrderByDescending(x => x.Permission.Name)
                    : query.OrderBy(x => x.Permission.Name),

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
                rolePermissions = await query
                   .Skip((dto.PageNumber - 1) * dto.PageSize)
                   .Take(dto.PageSize)
                   .ToListAsync();

            }
            else
            {
                rolePermissions =
                    await query.ToListAsync();

            }


            return (rolePermissions, totalRecords);
        }

    }
}
