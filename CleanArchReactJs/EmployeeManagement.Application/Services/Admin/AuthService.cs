using AutoMapper;
using EmployeeManagement.Application.DTOs.Admin.Auth;
using EmployeeManagement.Application.DTOs.Admin.Users;
using EmployeeManagement.Application.Interfaces.Admin;
using EmployeeManagement.Domain.Entities.Admin;
using EmployeeManagement.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Services.Admin
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            IUnitOfWork unitOfWork,
            ITokenService tokenService,
            IMapper mapper,
            ILogger<AuthService> logger)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task RegisterAsync(RegisterDto dto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation(
                "Registering user {UserName}",
                dto.UserName);

            var existingUser =
                await _unitOfWork.Users.GetByNameAsync( dto.UserName,cancellationToken);

            if (existingUser != null)
                throw new InvalidOperationException(
                    "Username already exists.");

            var emailExists =
                await _unitOfWork.Users.GetByEmailAsync(
                    dto.Email,
                    cancellationToken);

            if (emailExists != null)
                throw new InvalidOperationException(
                    "Email already exists.");

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                UserName = dto.UserName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),                
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "System"
            };

            await _unitOfWork.Users.AddAsync(user, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "User {UserName} registered successfully.",
                dto.UserName);
        }
        public async Task<LoginResponseDto> LoginAsync(
     LoginDto dto,
     CancellationToken cancellationToken = default)
        {
            _logger.LogInformation(
                "Login attempt for {UserNameOrEmail}",
                dto.UserNameOrEmail);

            User? user;

            if (dto.UserNameOrEmail.Contains("@"))
            {
                user = await _unitOfWork.Users.GetByEmailAsync(
                    dto.UserNameOrEmail,
                    cancellationToken);
            }
            else
            {
                user = await _unitOfWork.Users.GetByNameAsync(
                    dto.UserNameOrEmail,
                    cancellationToken);
            }

            if (user == null)
            {
                _logger.LogWarning( "Invalid login attempt for {UserNameOrEmail}", 
                    dto.UserNameOrEmail);
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "Invalid username or password."
                };
            }

            var passwordValid = BCrypt.Net.BCrypt.Verify(
                dto.Password,
                user.PasswordHash);

            if (!passwordValid)
            {
                _logger.LogWarning("Invalid login attempt for {UserNameOrEmail}",
                    dto.UserNameOrEmail);
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "Invalid username or password."
                };
            }

            var accessToken = _tokenService.GenerateAccessToken(user);

            var activeToken = await _unitOfWork.RefreshTokens.GetByUserIdAsync(user.Id);
            RefreshToken refreshToken = new RefreshToken();
            if (activeToken!=null && activeToken.Id != Guid.Empty)
            {
                if (activeToken.IsActive == false)
                {
                    _unitOfWork.RefreshTokens.Delete(activeToken);
                    refreshToken = _tokenService.GenerateRefreshToken();
                    user.RefreshToken = refreshToken;
                }
                else
                {
                    refreshToken = activeToken;
                }
            }
            else
            {
                refreshToken = _tokenService.GenerateRefreshToken();
                user.RefreshToken = refreshToken;
            }

            user.ModifiedDate = DateTime.UtcNow;
            user.ModifiedBy = user.UserName;

            _unitOfWork.Users.Update(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation( "User {UserName} logged in successfully", 
                user.UserName);
            return new LoginResponseDto
            {
                Success = true,
                Message = "Login successful.",

                AccessToken = accessToken,

                RefreshToken = refreshToken.Token,

                AccessTokenExpiration =
                    DateTime.UtcNow.AddMinutes(30),

                RefreshTokenExpiration =
                    refreshToken.Expires,

                User = _mapper.Map<UserDto>(user)
            };
        }
        public async Task<LoginResponseDto> RefreshTokenAsync(
      RefreshTokenDto dto,
      CancellationToken cancellationToken = default)
        {
            var principal =
                _tokenService.GetPrincipalFromExpiredToken(
                    dto.AccessToken);

            var username =
                principal.Identity?.Name;

            if (string.IsNullOrWhiteSpace(username))
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "Invalid access token."
                };
            }

            var user =
                await _unitOfWork.Users.GetByNameAsync(
                    username,
                    cancellationToken);

            if (user == null)
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            if (!_tokenService.ValidateRefreshToken(
                    user,
                    dto.RefreshToken))
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "Invalid refresh token."
                };
            }

            var newAccessToken =
                _tokenService.GenerateAccessToken(user);

            var newRefreshToken =
                _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;

            _unitOfWork.Users.Update(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new LoginResponseDto
            {
                Success = true,

                Message = "Token refreshed successfully.",

                AccessToken = newAccessToken,

                RefreshToken = newRefreshToken.Token,

                AccessTokenExpiration =
                    DateTime.UtcNow.AddMinutes(30),

                RefreshTokenExpiration =
                    newRefreshToken.Expires,

                User = _mapper.Map<UserDto>(user)
            };
        }
        public async Task LogoutAsync( Guid userId, CancellationToken cancellationToken = default)
        {
            var user =
                await _unitOfWork.Users.GetByIdAsync(
                    userId,
                    cancellationToken);

            if (user == null)
                return;

            if (user.RefreshToken != null)
            {
                user.RefreshToken.Revoked = DateTime.UtcNow;
            }

            _unitOfWork.Users.Update(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

    }
}
