using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<IEnumerable<UserDto>>> GetAllAsync();
        Task<ApiResponse<UserDto>> GetByIdAsync(Guid id);
        Task<ApiResponse<string>> CreateAsync(CreateUserDto dto);
        Task<ApiResponse<string>> UpdateAsync(UpdateUserDto dto);
        Task<ApiResponse<string>> DeleteAsync(Guid id);
        Task<ApiResponse<string>> DeletePermanentAsync(Guid id);
        Task<ApiResponse<string>> RestoreAsync(Guid id);
        Task<ApiResponse<PagedUserResponseDto>> SearchAsync(SearchUserDto dto);
    }
}
