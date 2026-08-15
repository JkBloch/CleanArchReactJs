using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.DTOs.Admin.UserRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Interfaces.Admin
{
    public interface IUserRoleService
    {
        Task<ApiResponse<IEnumerable<UserRoleDto>>> GetAllAsync();
        Task<ApiResponse<UserRoleDto>> GetByIdAsync(Guid id);
        Task<ApiResponse<string>> CreateAsync(CreateUserRoleDto dto);
        Task<ApiResponse<string>> UpdateAsync(UpdateUserRoleDto dto);
        Task<ApiResponse<string>> DeleteAsync(Guid id);
        Task<ApiResponse<string>> DeletePermanentAsync(Guid id);
        Task<ApiResponse<string>> RestoreAsync(Guid id);
        Task<ApiResponse<PagedUserRoleResponseDto>> SearchAsync(SearchUserRoleDto dto);
    }
}
