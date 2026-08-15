using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.DTOs.Admin.Permissions;
using EmployeeManagement.Application.DTOs.Admin.RolePermissions;
using EmployeeManagement.Application.DTOs.Admin.Roles;
using EmployeeManagement.Application.DTOs.Admin.UserRoles;
using EmployeeManagement.Application.DTOs.Admin.Users;
using EmployeeManagement.Application.DTOs.Master.City;
using EmployeeManagement.Application.DTOs.Master.Department;
using EmployeeManagement.Application.DTOs.Master.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Interfaces
{
    public interface IExcelExportService
    {
        Task<byte[]> ExportPermissionsAsync(SearchPermissionDto request);
        Task<byte[]> ExportRolesAsync(SearchRoleDto request);
        Task<byte[]> ExportRolePermissionsAsync(SearchRolePermissionDto request);
        Task<byte[]> ExportUsersAsync(SearchUserDto request);
        Task<byte[]> ExportUserRolesAsync(SearchUserRoleDto request);
        Task<byte[]> ExportStatesAsync(SearchStateDto request);
        Task<byte[]> ExportCitiesAsync(SearchCityDto request);
        Task<byte[]> ExportDepartmentsAsync(SearchDepartmentDto request);
    }
}
