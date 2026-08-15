using AutoMapper;
using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.DTOs.Admin.UserRoles;
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
    public class UserRoleService : IUserRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UserRoleService> _logger;
        public UserRoleService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserRoleService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ApiResponse<IEnumerable<UserRoleDto>>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Loading all userRoles.");

                var userRoles =
                    await _unitOfWork.UserRoles.GetAllAsync();

                var result =
                    _mapper.Map<IEnumerable<UserRoleDto>>(userRoles);

                return ApiResponse<IEnumerable<UserRoleDto>>
                    .Ok(result, "UserRoles loaded successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while loading userRoles.");

                return ApiResponse<IEnumerable<UserRoleDto>>
                    .Fail("Unable to load userRoles.");
            }
        }
        public async Task<ApiResponse<UserRoleDto>> GetByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(
                    "Loading userRole {UserRoleId}", id);

                //var userRole = await _unitOfWork.UserRoles.GetByIdAsync(id);
                var userRole = await _unitOfWork.UserRoles.GetUserRoleByIdAsync(id);

                if (userRole == null)
                {
                    return ApiResponse<UserRoleDto>
                        .Fail("UserRole not found.");
                }

                var dto = _mapper.Map<UserRoleDto>(userRole);

                return ApiResponse<UserRoleDto>
                    .Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error loading userRole {UserRoleId}",
                    id);

                return ApiResponse<UserRoleDto>
                    .Fail("Unable to load userRole.");
            }
        }
        public async Task<ApiResponse<string>> CreateAsync(CreateUserRoleDto dto)
        {
            try
            {
                _logger.LogInformation(
                    "Creating userRole {RoleId}",
                    dto.RoleId);


                var userRolelExists = await _unitOfWork.UserRoles.UserRoleExistsAsync(dto.RoleId, dto.UserId, Guid.Empty);

                if (userRolelExists)
                {
                    return ApiResponse<string>.Fail(
                        "UserRole  already exists.");
                }




                var userRole = _mapper.Map<UserRole>(dto);

                await _unitOfWork.UserRoles.AddAsync(userRole);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "UserRole {UserRoleCode} created successfully.",
                    dto.RoleId);

                return ApiResponse<string>.Ok(
                    userRole.Id.ToString(),
                    "UserRole created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error creating userRole.");

                return ApiResponse<string>.Fail(
                    "Unable to create userRole.");
            }
        }
        public async Task<ApiResponse<string>> UpdateAsync(UpdateUserRoleDto dto)
        {
            try
            {
                _logger.LogInformation(
                    "Updating userRole {UserRoleId}",
                    dto.Id);

                var userRole = await _unitOfWork.UserRoles
                        .GetByIdAsync(dto.Id);

                if (userRole == null)
                {
                    return ApiResponse<string>
                        .Fail("UserRole not found.");
                }

                var userRolelExists = await _unitOfWork.UserRoles.UserRoleExistsAsync(dto.RoleId, dto.UserId, dto.Id);

                if (userRolelExists)
                {
                    return ApiResponse<string>
                        .Fail("UserRole already exists.");
                }
                _mapper.Map(dto, userRole);

                userRole.ModifiedDate = DateTime.UtcNow;
                userRole.ModifiedBy = "System";

                _unitOfWork.UserRoles.Update(userRole);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "UserRole {UserRoleId} updated successfully.",
                    userRole.Id);

                return ApiResponse<string>.Ok(
                    userRole.Id.ToString(),
                    "UserRole updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error updating userRole {UserRoleId}",
                    dto.Id);

                return ApiResponse<string>
                    .Fail("Unable to update userRole.");
            }
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(
                    "Deleting userRole {UserRoleId}",
                    id);

                var userRole = await _unitOfWork.UserRoles.GetByIdAsync(id);

                if (userRole == null)
                {
                    return ApiResponse<string>
                        .Fail("UserRole not found.");
                }

                userRole.IsDeleted = true;
                userRole.ModifiedDate = DateTime.UtcNow;
                userRole.ModifiedBy = "System";

                _unitOfWork.UserRoles.Update(userRole);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "UserRole {UserRoleId} deleted successfully.",
                    id);

                return ApiResponse<string>.Ok(
                    userRole.Id.ToString(),
                    "UserRole deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error deleting userRole {UserRoleId}",
                    id);

                return ApiResponse<string>
                    .Fail("Unable to delete userRole.");
            }
        }
        public async Task<ApiResponse<string>> DeletePermanentAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(
                    "Deleting userRole {UserRoleId}",
                    id);

                var userRole = await _unitOfWork.UserRoles.GetByIdAsync(id);

                if (userRole == null)
                {
                    return ApiResponse<string>
                        .Fail("UserRole not found.");
                }

                _unitOfWork.UserRoles.Delete(userRole);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "UserRole {UserRoleId} deleted successfully.",
                    id);

                return ApiResponse<string>.Ok(
                    userRole.Id.ToString(),
                    "UserRole deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error deleting userRole {UserRoleId}",
                    id);

                return ApiResponse<string>
                    .Fail("Unable to delete userRole.");
            }
        }

        public async Task<ApiResponse<string>> RestoreAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(
                    "Restoring userRole {UserRoleId}",
                    id);

                var userRole = await _unitOfWork.UserRoles.GetDeletedUserRoleAsync(id);

                if (userRole == null)
                {
                    return ApiResponse<string>.Fail(
                        "UserRole not found.");
                }

                if (!userRole.IsDeleted)
                {
                    return ApiResponse<string>.Fail(
                        "UserRole is already active.");
                }

                userRole.IsDeleted = false;
                userRole.ModifiedDate = DateTime.UtcNow;
                userRole.ModifiedBy = "System";

                _unitOfWork.UserRoles.Update(userRole);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "UserRole restored successfully.");

                return ApiResponse<string>.Ok(
                    userRole.Id.ToString(),
                    "UserRole restored successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Restore userRole failed.");

                return ApiResponse<string>.Fail(
                    "Unable to restore userRole.");
            }
        }
        public async Task<ApiResponse<PagedUserRoleResponseDto>> SearchAsync(SearchUserRoleDto dto)
        {
            try
            {
                IQueryable<UserRole> query = _unitOfWork.UserRoles.Query()
                    .Include(x => x.Role).Include(x => x.User);

                //-------------------------
                // Keyword Search
                //-------------------------

                if (!string.IsNullOrWhiteSpace(dto.Keyword))
                {
                    var keyword = dto.Keyword.Trim();

                    query = query.Where(x =>
                        EF.Functions.Like(x.Role.Name, $"%{keyword}%") ||
                        EF.Functions.Like(x.User.UserName, $"%{keyword}%"));
                }
                if (dto.RoleId != null && dto.RoleId != Guid.Empty)
                {
                    query = query.Where(x => x.RoleId == dto.RoleId);
                }
                if (dto.UserId != null && dto.UserId != Guid.Empty)
                {
                    query = query.Where(x => x.UserId == dto.UserId);
                }
                //-------------------------
                // Sorting
                //-------------------------

                query = dto.SortBy?.ToLower() switch
                {
                    "role" => dto.Descending
                        ? query.OrderByDescending(x => x.Role.Name)
                        : query.OrderBy(x => x.Role.Name),

                    "permission" => dto.Descending
                        ? query.OrderByDescending(x => x.User.UserName)
                        : query.OrderBy(x => x.User.UserName),

                    _ => dto.Descending
                        ? query.OrderByDescending(x => x.Role.Name)
                        : query.OrderBy(x => x.Role.Name)
                };

                //-------------------------
                // Count
                //-------------------------

                var totalRecords = await query.CountAsync();

                //-------------------------
                // Paging
                //-------------------------

                dto.PageSize = Math.Min(dto.PageSize, 100);

                var userRoles = await query
                    .Skip((dto.PageNumber - 1) * dto.PageSize)
                    .Take(dto.PageSize)
                    .ToListAsync();

                //-------------------------
                // Response
                //-------------------------

                var response = new PagedUserRoleResponseDto
                {
                    Items = _mapper.Map<List<UserRoleDto>>(userRoles),
                    TotalRecords = totalRecords,
                    PageNumber = dto.PageNumber,
                    PageSize = dto.PageSize
                    //TotalPages = (int)Math.Ceiling(
                    //    totalRecords / (double)dto.PageSize)
                };

                return ApiResponse<PagedUserRoleResponseDto>.Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UserRole search failed.");

                return ApiResponse<PagedUserRoleResponseDto>.Fail(
                    "Unable to search userRoles.");
            }
        }

    }

}
