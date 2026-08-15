using EmployeeManagement.Application.DTOs.Admin.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Interfaces.Admin
{
    public interface IAuthService
    {

        Task<LoginResponseDto> LoginAsync( LoginDto dto, CancellationToken cancellationToken = default);

        Task RegisterAsync( RegisterDto dto, CancellationToken cancellationToken = default);

        Task<LoginResponseDto> RefreshTokenAsync( RefreshTokenDto dto, CancellationToken cancellationToken = default);

        Task LogoutAsync( Guid userId, CancellationToken cancellationToken = default);

    }
}
