using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.DTOs.Master.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Interfaces.Master
{
    public interface IDepartmentService
    {
        Task<ApiResponse<IEnumerable<DepartmentDto>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ApiResponse<DepartmentDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ApiResponse<string>> CreateAsync(CreateDepartmentDto dto, CancellationToken cancellationToken = default);
        Task<ApiResponse<string>> UpdateAsync(UpdateDepartmentDto dto, CancellationToken cancellationToken = default);
        Task<ApiResponse<string>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ApiResponse<string>> DeletePermanentAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ApiResponse<string>> RestoreAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ApiResponse<PagedDepartmentResponseDto>> SearchAsync(SearchDepartmentDto dto, CancellationToken cancellationToken = default);
    }
}
