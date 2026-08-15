using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.DTOs.Master.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Interfaces.Master
{
    public interface IStateService
    {
        Task<ApiResponse<IEnumerable<StateDto>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ApiResponse<StateDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ApiResponse<string>> CreateAsync(CreateStateDto dto, CancellationToken cancellationToken = default);
        Task<ApiResponse<string>> UpdateAsync(UpdateStateDto dto, CancellationToken cancellationToken = default);
        Task<ApiResponse<string>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ApiResponse<string>> DeletePermanentAsync(Guid id , CancellationToken cancellationToken = default);
        Task<ApiResponse<string>> RestoreAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ApiResponse<PagedStateResponseDto>> SearchAsync(SearchStateDto dto, CancellationToken cancellationToken = default);
    }
}
