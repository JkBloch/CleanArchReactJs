using AutoMapper;
using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.DTOs.RolePermissions;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Services
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<RolePermissionService> _logger;
        public RolePermissionService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<RolePermissionService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ApiResponse<IEnumerable<RolePermissionDto>>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Loading all rolePermissions.");

                var rolePermissions =
                    await _unitOfWork.RolePermissions.GetAllAsync();

                var result =
                    _mapper.Map<IEnumerable<RolePermissionDto>>(rolePermissions);

                return ApiResponse<IEnumerable<RolePermissionDto>>
                    .Ok(result, "RolePermissions loaded successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while loading rolePermissions.");

                return ApiResponse<IEnumerable<RolePermissionDto>>
                    .Fail("Unable to load rolePermissions.");
            }
        }
        public async Task<ApiResponse<RolePermissionDto>> GetByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(
                    "Loading rolePermission {RolePermissionId}", id);

                //var rolePermission = await _unitOfWork.RolePermissions.GetByIdAsync(id);
                var rolePermission = await _unitOfWork.RolePermissions.GetRolePermissionByIdAsync(id);

                if (rolePermission == null)
                {
                    return ApiResponse<RolePermissionDto>
                        .Fail("RolePermission not found.");
                }

                var dto = _mapper.Map<RolePermissionDto>(rolePermission);

                return ApiResponse<RolePermissionDto>
                    .Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error loading rolePermission {RolePermissionId}",
                    id);

                return ApiResponse<RolePermissionDto>
                    .Fail("Unable to load rolePermission.");
            }
        }
        public async Task<ApiResponse<string>> CreateAsync(CreateRolePermissionDto dto)
        {
            try
            {
                _logger.LogInformation(
                    "Creating rolePermission {RoleId}",
                    dto.RoleId);


                var rolePermissionlExists = await _unitOfWork.RolePermissions.RolePermissionExistsAsync(dto.RoleId,dto.PermissionId,Guid.Empty);

                if (rolePermissionlExists)
                {
                    return ApiResponse<string>.Fail(
                        "RolePermission  already exists.");
                }

                 


                var rolePermission = _mapper.Map<RolePermission>(dto);

                await _unitOfWork.RolePermissions.AddAsync(rolePermission);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "RolePermission {RolePermissionCode} created successfully.",
                    dto.RoleId);

                return ApiResponse<string>.Ok(
                    rolePermission.Id.ToString(),
                    "RolePermission created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error creating rolePermission.");

                return ApiResponse<string>.Fail(
                    "Unable to create rolePermission.");
            }
        }
        public async Task<ApiResponse<string>> UpdateAsync(UpdateRolePermissionDto dto)
        {
            try
            {
                _logger.LogInformation(
                    "Updating rolePermission {RolePermissionId}",
                    dto.Id);

                var rolePermission = await _unitOfWork.RolePermissions
                        .GetByIdAsync(dto.Id);

                if (rolePermission == null)
                {
                    return ApiResponse<string>
                        .Fail("RolePermission not found.");
                }

                var rolePermissionlExists = await _unitOfWork.RolePermissions.RolePermissionExistsAsync(dto.RoleId, dto.PermissionId, dto.Id);

                if (rolePermissionlExists)
                {
                    return ApiResponse<string>
                        .Fail("RolePermission already exists.");
                }
                _mapper.Map(dto, rolePermission);

                rolePermission.ModifiedDate = DateTime.UtcNow;
                rolePermission.ModifiedBy = "System";

                _unitOfWork.RolePermissions.Update(rolePermission);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "RolePermission {RolePermissionId} updated successfully.",
                    rolePermission.Id);

                return ApiResponse<string>.Ok(
                    rolePermission.Id.ToString(),
                    "RolePermission updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error updating rolePermission {RolePermissionId}",
                    dto.Id);

                return ApiResponse<string>
                    .Fail("Unable to update rolePermission.");
            }
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(
                    "Deleting rolePermission {RolePermissionId}",
                    id);

                var rolePermission = await _unitOfWork.RolePermissions.GetByIdAsync(id);

                if (rolePermission == null)
                {
                    return ApiResponse<string>
                        .Fail("RolePermission not found.");
                }

                rolePermission.IsDeleted = true;
                rolePermission.ModifiedDate = DateTime.UtcNow;
                rolePermission.ModifiedBy = "System";

                _unitOfWork.RolePermissions.Update(rolePermission);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "RolePermission {RolePermissionId} deleted successfully.",
                    id);

                return ApiResponse<string>.Ok(
                    rolePermission.Id.ToString(),
                    "RolePermission deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error deleting rolePermission {RolePermissionId}",
                    id);

                return ApiResponse<string>
                    .Fail("Unable to delete rolePermission.");
            }
        }
        public async Task<ApiResponse<string>> DeletePermanentAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(
                    "Deleting rolePermission {RolePermissionId}",
                    id);

                var rolePermission = await _unitOfWork.RolePermissions.GetByIdAsync(id);

                if (rolePermission == null)
                {
                    return ApiResponse<string>
                        .Fail("RolePermission not found.");
                }

                _unitOfWork.RolePermissions.Delete(rolePermission);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "RolePermission {RolePermissionId} deleted successfully.",
                    id);

                return ApiResponse<string>.Ok(
                    rolePermission.Role.Name + rolePermission.Permission.Name,
                    "RolePermission deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error deleting rolePermission {RolePermissionId}",
                    id);

                return ApiResponse<string>
                    .Fail("Unable to delete rolePermission.");
            }
        }

        public async Task<ApiResponse<string>> RestoreAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(
                    "Restoring rolePermission {RolePermissionId}",
                    id);

                var rolePermission = await _unitOfWork.RolePermissions.GetDeletedRolePermissionAsync(id);

                if (rolePermission == null)
                {
                    return ApiResponse<string>.Fail(
                        "RolePermission not found.");
                }

                if (!rolePermission.IsDeleted)
                {
                    return ApiResponse<string>.Fail(
                        "RolePermission is already active.");
                }

                rolePermission.IsDeleted = false;
                rolePermission.ModifiedDate = DateTime.UtcNow;
                rolePermission.ModifiedBy = "System";

                _unitOfWork.RolePermissions.Update(rolePermission);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "RolePermission restored successfully.");

                return ApiResponse<string>.Ok(
                    rolePermission.Id.ToString(),
                    "RolePermission restored successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Restore rolePermission failed.");

                return ApiResponse<string>.Fail(
                    "Unable to restore rolePermission.");
            }
        }
        public async Task<ApiResponse<PagedRolePermissionResponseDto>> SearchAsync(SearchRolePermissionDto dto)
        {
            try
            {
                IQueryable<RolePermission> query = _unitOfWork.RolePermissions.Query()
                    .Include(x=>x.Role).Include(x=>x.Permission);

                //-------------------------
                // Keyword Search
                //-------------------------

                if (!string.IsNullOrWhiteSpace(dto.Keyword))
                {
                    var keyword = dto.Keyword.Trim();

                    query = query.Where(x =>
                        EF.Functions.Like(x.Role.Name, $"%{keyword}%") ||
                        EF.Functions.Like(x.Permission.Name, $"%{keyword}%"));
                }
                if (dto.RoleId != null && dto.RoleId != Guid.Empty)
                {
                    query = query.Where(x =>x.RoleId== dto.RoleId);
                }
                if (dto.PermissionId != null && dto.PermissionId != Guid.Empty)
                {
                    query = query.Where(x => x.PermissionId == dto.PermissionId);
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
                        ? query.OrderByDescending(x => x.Permission.Name)
                        : query.OrderBy(x => x.Permission.Name),

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

                var rolePermissions = await query
                    .Skip((dto.PageNumber - 1) * dto.PageSize)
                    .Take(dto.PageSize)
                    .ToListAsync();

                //-------------------------
                // Response
                //-------------------------

                var response = new PagedRolePermissionResponseDto
                {
                    Items = _mapper.Map<List<RolePermissionDto>>(rolePermissions),
                    TotalRecords = totalRecords,
                    PageNumber = dto.PageNumber,
                    PageSize = dto.PageSize
                    //TotalPages = (int)Math.Ceiling(
                    //    totalRecords / (double)dto.PageSize)
                };

                return ApiResponse<PagedRolePermissionResponseDto>.Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RolePermission search failed.");

                return ApiResponse<PagedRolePermissionResponseDto>.Fail(
                    "Unable to search rolePermissions.");
            }
        }

    }

}
