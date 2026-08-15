using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.DTOs.Admin.RolePermissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Interfaces.Admin
{
    public interface IRolePermissionService
    {
        Task<ApiResponse<IEnumerable<RolePermissionDto>>> GetAllAsync();
        Task<ApiResponse<RolePermissionDto>> GetByIdAsync(Guid id);
        Task<ApiResponse<string>> CreateAsync(CreateRolePermissionDto dto);
        Task<ApiResponse<string>> UpdateAsync(UpdateRolePermissionDto dto);
        Task<ApiResponse<string>> DeleteAsync(Guid id);
        Task<ApiResponse<string>> DeletePermanentAsync(Guid id);
        Task<ApiResponse<string>> RestoreAsync(Guid id);
        Task<ApiResponse<PagedRolePermissionResponseDto>> SearchAsync(SearchRolePermissionDto dto);
    }
}
