using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.DTOs.Master.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Interfaces.Master
{
    public interface ICityService
    {
        Task<ApiResponse<IEnumerable<CityDto>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ApiResponse<CityDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ApiResponse<string>> CreateAsync(CreateCityDto dto, CancellationToken cancellationToken = default);
        Task<ApiResponse<string>> UpdateAsync(UpdateCityDto dto, CancellationToken cancellationToken = default);
        Task<ApiResponse<string>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ApiResponse<string>> DeletePermanentAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ApiResponse<string>> RestoreAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ApiResponse<PagedCityResponseDto>> SearchAsync(SearchCityDto dto, CancellationToken cancellationToken = default);
    }
}
