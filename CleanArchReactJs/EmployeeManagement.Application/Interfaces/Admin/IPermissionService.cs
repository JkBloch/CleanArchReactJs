using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.DTOs.Admin.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Interfaces.Admin
{
    public interface IPermissionService
    {
        Task<ApiResponse<IEnumerable<PermissionDto>>> GetAllAsync();
        Task<ApiResponse<PermissionDto>> GetByIdAsync(Guid id);
        Task<ApiResponse<string>> CreateAsync(CreatePermissionDto dto);
        Task<ApiResponse<string>> UpdateAsync(UpdatePermissionDto dto);
        Task<ApiResponse<string>> DeleteAsync(Guid id);
        Task<ApiResponse<string>> DeletePermanentAsync(Guid id);
        Task<ApiResponse<string>> RestoreAsync(Guid id);       
        Task<ApiResponse<PagedPermissionResponseDto>> SearchAsync(SearchPermissionDto dto);
    }
}
