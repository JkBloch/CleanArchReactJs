using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.DTOs.Admin.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Interfaces.Admin
{
    public interface IRoleService
    {
        Task<ApiResponse<IEnumerable<RoleDto>>> GetAllAsync();
        Task<ApiResponse<RoleDto>> GetByIdAsync(Guid id);
        Task<ApiResponse<string>> CreateAsync(CreateRoleDto dto);
        Task<ApiResponse<string>> UpdateAsync(UpdateRoleDto dto);
        Task<ApiResponse<string>> DeleteAsync(Guid id);
        Task<ApiResponse<string>> DeletePermanentAsync(Guid id);
        Task<ApiResponse<string>> RestoreAsync(Guid id);
        Task<ApiResponse<PagedRoleResponseDto>> SearchAsync(SearchRoleDto dto);
    }
}
