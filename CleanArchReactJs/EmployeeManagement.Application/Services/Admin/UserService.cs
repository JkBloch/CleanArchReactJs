using AutoMapper;
using DocumentFormat.OpenXml.Math;
using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.Common.SearchExport.Admin;
using EmployeeManagement.Application.DTOs.Admin.Users;
using EmployeeManagement.Application.Interfaces.Admin;
using EmployeeManagement.Domain.Entities.Admin;
using EmployeeManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Services.Admin
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ApiResponse<IEnumerable<UserDto>>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Loading all users.");

                var users =
                    await _unitOfWork.Users.GetAllAsync();

                var result =
                    _mapper.Map<IEnumerable<UserDto>>(users);

                return ApiResponse<IEnumerable<UserDto>>
                    .Ok(result, "Users loaded successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while loading users.");

                return ApiResponse<IEnumerable<UserDto>>
                    .Fail("Unable to load users.");
            }
        }
        public async Task<ApiResponse<UserDto>> GetByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(
                    "Loading user {UserId}", id);

                var user = await _unitOfWork.Users.GetByIdAsync(id);

                if (user == null)
                {
                    return ApiResponse<UserDto>
                        .Fail("User not found.");
                }

                var dto = _mapper.Map<UserDto>(user);

                return ApiResponse<UserDto>
                    .Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error loading user {UserId}",
                    id);

                return ApiResponse<UserDto>
                    .Fail("Unable to load user.");
            }
        }
        public async Task<ApiResponse<string>> CreateAsync(CreateUserDto dto)
        {
            try
            {
                _logger.LogInformation(
                    "Creating user {UserName}",
                    dto.UserName);

                // USerName validation
                var nameExists = await _unitOfWork.Users.GetByNameAsync(dto.UserName);

                if (nameExists != null && nameExists.Id != Guid.Empty)
                {
                    return ApiResponse<string>.Fail(
                        "UserName already exists.");
                }

                // Email validation
                var emailExists = await _unitOfWork.Users.GetByEmailAsync(dto.Email);

                if (emailExists != null && emailExists.Id != Guid.Empty)
                {
                    return ApiResponse<string>.Fail(
                        "Email already exists.");
                }

                dto.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

                var user = _mapper.Map<User>(dto);

                await _unitOfWork.Users.AddAsync(user);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "User {UserName} created successfully.",
                    dto.UserName);

                return ApiResponse<string>.Ok(
                    user.UserName,
                    "User created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error creating user.");

                return ApiResponse<string>.Fail(
                    "Unable to create user.");
            }
        }
        public async Task<ApiResponse<string>> UpdateAsync(UpdateUserDto dto)
        {
            try
            {
                _logger.LogInformation(
                    "Updating user {UserId}",
                    dto.Id);

                var user = await _unitOfWork.Users
                        .GetByIdAsync(dto.Id);

                if (user == null)
                {
                    return ApiResponse<string>
                        .Fail("User not found.");
                }

                if (await _unitOfWork.Users.NameExistsAsync(dto.UserName, dto.Id))
                {
                    return ApiResponse<string>
                        .Fail("UserName already exists.");
                }

                if (await _unitOfWork.Users.EmailExistsAsync(dto.Email, dto.Id))
                {
                    return ApiResponse<string>
                        .Fail("Email already exists.");
                }

                _mapper.Map(dto, user);

                user.ModifiedDate = DateTime.UtcNow;
                user.ModifiedBy = "System";

                _unitOfWork.Users.Update(user);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "User {UserId} updated successfully.",
                    user.Id);

                return ApiResponse<string>.Ok(
                    user.UserName,
                    "User updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error updating user {UserId}",
                    dto.Id);

                return ApiResponse<string>
                    .Fail("Unable to update user.");
            }
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(
                    "Deleting user {UserId}",
                    id);

                var user = await _unitOfWork.Users.GetByIdAsync(id);

                if (user == null)
                {
                    return ApiResponse<string>
                        .Fail("User not found.");
                }

                user.IsDeleted = true;
                user.ModifiedDate = DateTime.UtcNow;
                user.ModifiedBy = "System";

                _unitOfWork.Users.Update(user);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "User {UserId} deleted successfully.",
                    id);

                return ApiResponse<string>.Ok(
                    user.UserName,
                    "User deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error deleting user {UserId}",
                    id);

                return ApiResponse<string>
                    .Fail("Unable to delete user.");
            }
        }
        public async Task<ApiResponse<string>> DeletePermanentAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(
                    "Deleting user {UserId}",
                    id);

                var user = await _unitOfWork.Users.GetByIdAsync(id);

                if (user == null)
                {
                    return ApiResponse<string>
                        .Fail("User not found.");
                }

                _unitOfWork.Users.Delete(user);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "User {UserId} deleted successfully.",
                    id);

                return ApiResponse<string>.Ok(
                    user.UserName,
                    "User deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error deleting user {UserId}",
                    id);

                return ApiResponse<string>
                    .Fail("Unable to delete user.");
            }
        }

        public async Task<ApiResponse<string>> RestoreAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(
                    "Restoring user {UserId}",
                    id);

                var user = await _unitOfWork.Users.GetDeletedUserAsync(id);

                if (user == null)
                {
                    return ApiResponse<string>.Fail(
                        "User not found.");
                }

                if (!user.IsDeleted)
                {
                    return ApiResponse<string>.Fail(
                        "User is already active.");
                }

                user.IsDeleted = false;
                user.ModifiedDate = DateTime.UtcNow;
                user.ModifiedBy = "System";

                _unitOfWork.Users.Update(user);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "User restored successfully.");

                return ApiResponse<string>.Ok(
                    user.UserName,
                    "User restored successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Restore user failed.");

                return ApiResponse<string>.Fail(
                    "Unable to restore user.");
            }
        }
        public async Task<ApiResponse<PagedUserResponseDto>> SearchAsync(SearchUserDto dto)
        {
            try
            {
                IQueryable<User> query = _unitOfWork.Users.Query();
                var (users, totalRecords) = await UserSearchData.GetExportUserData(query, dto, "page");              

                //-------------------------
                // Response
                //-------------------------

                var response = new PagedUserResponseDto
                {
                    Items = _mapper.Map<List<UserDto>>(users),
                    TotalRecords = totalRecords,
                    PageNumber = dto.PageNumber,
                    PageSize = dto.PageSize
                    //TotalPages = (int)Math.Ceiling(
                    //    totalRecords / (double)dto.PageSize)
                };

                return ApiResponse<PagedUserResponseDto>.Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "User search failed.");

                return ApiResponse<PagedUserResponseDto>.Fail(
                    "Unable to search users.");
            }
        }

    }
}
