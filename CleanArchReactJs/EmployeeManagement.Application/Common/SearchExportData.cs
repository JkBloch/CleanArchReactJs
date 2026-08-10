using DocumentFormat.OpenXml.Office2016.Excel;
using EmployeeManagement.Application.DTOs.Permissions;
using EmployeeManagement.Application.DTOs.RolePermissions;
using EmployeeManagement.Application.DTOs.Roles;
using EmployeeManagement.Application.DTOs.UserRoles;
using EmployeeManagement.Application.DTOs.Users;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Common
{
    public static class SearchExportData
    {
        public async static Task<(List<Permission> permissions, int totalRecords)> GetExportPermissionData(IQueryable<Permission> query, SearchPermissionDto dto, string searchFor)
        {
            //-------------------------
            // Keyword Search
            //-------------------------
            List<Permission> permissions = new List<Permission>();

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
                 permissions = await query 
                    .Skip((dto.PageNumber - 1) * dto.PageSize) 
                    .Take(dto.PageSize) 
                    .ToListAsync();

            }
            else
            {
                permissions =
                    await query.ToListAsync();

            }
            return (permissions , totalRecords);
        }

        public async static Task<(List<Role> roles, int totalRecords)> GetExportRoleData(IQueryable<Role> query, SearchRoleDto dto, string searchFor)
        {
            //-------------------------
            // Keyword Search
            //-------------------------
            List<Role> roles = new List<Role>();

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
                roles = await query
                   .Skip((dto.PageNumber - 1) * dto.PageSize)
                   .Take(dto.PageSize)
                   .ToListAsync();

            }
            else
            {
                roles =
                    await query.ToListAsync();

            }
 

            return (roles, totalRecords);
        }

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
                    : query.OrderBy(x => x.Permission.Name ),

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
        public async static Task<(List<User> users, int totalRecords)> GetExportUserData(IQueryable<User> query, SearchUserDto dto, string searchFor)
        {
            //-------------------------
            // Keyword Search
            //-------------------------
            List<User> users = new List<User>();

            if (!string.IsNullOrWhiteSpace(dto.Keyword))
            {
                var keyword = dto.Keyword.Trim();

                query = query.Where(x =>
                    EF.Functions.Like(x.FirstName, $"%{keyword}%") ||
                    EF.Functions.Like(x.LastName, $"%{keyword}%") || 
                    EF.Functions.Like(x.UserName, $"%{keyword}%") || 
                    EF.Functions.Like(x.Email, $"%{keyword}%") ); 
            }
            if (!string.IsNullOrWhiteSpace(dto.FirstName))
            {
                var firsName = dto.FirstName.Trim();

                query = query.Where(x =>
                EF.Functions.Like(x.FirstName, $"{firsName}%"));
            }
            if (!string.IsNullOrWhiteSpace(dto.LastName))
            {
                var lastname = dto.LastName.Trim();

                query = query.Where(x =>
                EF.Functions.Like(x.LastName, $"{lastname}%"));
            }
            if (!string.IsNullOrWhiteSpace(dto.UserName))
            {
                var username = dto.UserName.Trim();

                query = query.Where(x =>
                EF.Functions.Like(x.UserName, $"{username}%"));
            }
            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                var email = dto.Email.Trim();

                query = query.Where(x =>
                EF.Functions.Like(x.Email, $"{email}%"));
            }


            //-------------------------
            // Sorting
            //-------------------------

            query = dto.SortBy?.ToLower() switch
            {
                "firstname" => dto.Descending
                    ? query.OrderByDescending(x => x.FirstName)
                    : query.OrderBy(x => x.FirstName),
                "lastname" => dto.Descending
                    ? query.OrderByDescending(x => x.LastName)
                    : query.OrderBy(x => x.LastName), 
                "username" => dto.Descending 
                    ? query.OrderByDescending(x => x.UserName) 
                    : query.OrderBy(x => x.UserName),
                "email" => dto.Descending
                    ? query.OrderByDescending(x => x.Email)
                    : query.OrderBy(x => x.Email),
                _ => dto.Descending
                    ? query.OrderByDescending(x => x.UserName)
                    : query.OrderBy(x => x.UserName)
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
                users = await query
                   .Skip((dto.PageNumber - 1) * dto.PageSize)
                   .Take(dto.PageSize)
                   .ToListAsync();

            }
            else
            {
                users =
                    await query.ToListAsync();

            }


            return (users, totalRecords);
        }
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
