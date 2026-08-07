using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.DTOs.Permissions;
using EmployeeManagement.Application.DTOs.RolePermissions;
using EmployeeManagement.Application.DTOs.Roles;
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
    }
}
