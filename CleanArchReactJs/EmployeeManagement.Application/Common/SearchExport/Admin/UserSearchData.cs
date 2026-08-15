using EmployeeManagement.Application.DTOs.Admin.Users;
using EmployeeManagement.Domain.Entities.Admin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Common.SearchExport.Admin
{
    public static class UserSearchData
    {
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
                    EF.Functions.Like(x.Email, $"%{keyword}%"));
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

    }
}
