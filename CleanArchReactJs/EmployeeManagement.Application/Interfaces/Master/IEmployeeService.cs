using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.DTOs.Master.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Interfaces.Master
{
    public interface IEmployeeService
    {
        Task<ApiResponse<IEnumerable<EmployeeDto>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ApiResponse<EmployeeDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ApiResponse<string>> CreateAsync(CreateEmployeeDto dto, CancellationToken cancellationToken = default);
        Task<ApiResponse<string>> UpdateAsync(UpdateEmployeeDto dto, CancellationToken cancellationToken = default);
        Task<ApiResponse<string>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ApiResponse<string>> DeletePermanentAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ApiResponse<string>> RestoreAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ApiResponse<PagedEmployeeResponseDto>> SearchAsync(SearchEmployeeDto dto, CancellationToken cancellationToken = default);
        Task<ApiResponse<string>> CreateDummyData(CancellationToken cancellationToken = default);
    }
}
