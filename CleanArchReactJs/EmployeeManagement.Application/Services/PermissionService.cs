using AutoMapper;
using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.DTOs.Permissions;
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
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PermissionService> _logger;
        public PermissionService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PermissionService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ApiResponse<IEnumerable<PermissionDto>>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Loading all permissions.");

                var permissions =
                    await _unitOfWork.Permissions.GetAllAsync();

                var result =
                    _mapper.Map<IEnumerable<PermissionDto>>(permissions);

                return ApiResponse<IEnumerable<PermissionDto>>
                    .Ok(result, "Permissions loaded successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while loading permissions.");

                return ApiResponse<IEnumerable<PermissionDto>>
                    .Fail("Unable to load permissions.");
            }
        }
        public async Task<ApiResponse<PermissionDto>> GetByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(
                    "Loading permission {PermissionId}", id);

                var permission = await _unitOfWork.Permissions.GetByIdAsync(id);

                if (permission == null)
                {
                    return ApiResponse<PermissionDto>
                        .Fail("Permission not found.");
                }

                var dto = _mapper.Map<PermissionDto>(permission);

                return ApiResponse<PermissionDto>
                    .Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error loading permission {PermissionId}",
                    id);

                return ApiResponse<PermissionDto>
                    .Fail("Unable to load permission.");
            }
        }
        public async Task<ApiResponse<string>> CreateAsync(CreatePermissionDto dto)
        {
            try
            {
                _logger.LogInformation(
                    "Creating permission {PermissionCode}",
                    dto.Code);

                // Email validation
                var nameExists = await _unitOfWork.Permissions.GetByNameAsync(dto.Name);

                if (nameExists != null && nameExists.Id != Guid.Empty)
                {
                    return ApiResponse<string>.Fail(
                        "Name already exists.");
                }

                // Permission Code validation
                var codeExists = await _unitOfWork.Permissions.GetByCodeAsync(dto.Code);

                if (codeExists != null && codeExists.Id != Guid.Empty)
                {
                    return ApiResponse<string>.Fail(
                        "Code already exists.");
                }


                var permission = _mapper.Map<Permission>(dto);

                await _unitOfWork.Permissions.AddAsync(permission);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "Permission {PermissionCode} created successfully.",
                    dto.Code);

                return ApiResponse<string>.Ok(
                    permission.Code,
                    "Permission created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error creating permission.");

                return ApiResponse<string>.Fail(
                    "Unable to create permission.");
            }
        }
        public async Task<ApiResponse<string>> UpdateAsync(UpdatePermissionDto dto)
        {
            try
            {
                _logger.LogInformation(
                    "Updating permission {PermissionId}",
                    dto.Id);

                var permission = await _unitOfWork.Permissions
                        .GetByIdAsync(dto.Id);

                if (permission == null)
                {
                    return ApiResponse<string>
                        .Fail("Permission not found.");
                }

                if (await _unitOfWork.Permissions.NameExistsAsync(dto.Name, dto.Id))
                {
                    return ApiResponse<string>
                        .Fail("Name already exists.");
                }

                if (await _unitOfWork.Permissions.CodeExistsAsync(dto.Code, dto.Id))
                {
                    return ApiResponse<string>
                        .Fail("Code already exists.");
                }

                _mapper.Map(dto, permission);

                permission.ModifiedDate = DateTime.UtcNow;
                permission.ModifiedBy = "System";

                _unitOfWork.Permissions.Update(permission);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "Permission {PermissionId} updated successfully.",
                    permission.Id);

                return ApiResponse<string>.Ok(
                    permission.Code,
                    "Permission updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error updating permission {PermissionId}",
                    dto.Id);

                return ApiResponse<string>
                    .Fail("Unable to update permission.");
            }
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(
                    "Deleting permission {PermissionId}",
                    id);

                var permission = await _unitOfWork.Permissions.GetByIdAsync(id);

                if (permission == null)
                {
                    return ApiResponse<string>
                        .Fail("Permission not found.");
                }

                permission.IsDeleted = true;
                permission.ModifiedDate = DateTime.UtcNow;
                permission.ModifiedBy = "System";

                _unitOfWork.Permissions.Update(permission);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "Permission {PermissionId} deleted successfully.",
                    id);

                return ApiResponse<string>.Ok(
                    permission.Code,
                    "Permission deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error deleting permission {PermissionId}",
                    id);

                return ApiResponse<string>
                    .Fail("Unable to delete permission.");
            }
        }
        public async Task<ApiResponse<string>> DeletePermanentAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(
                    "Deleting permission {PermissionId}",
                    id);

                var permission = await _unitOfWork.Permissions.GetByIdAsync(id);

                if (permission == null)
                {
                    return ApiResponse<string>
                        .Fail("Permission not found.");
                }

                _unitOfWork.Permissions.Delete(permission);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "Permission {PermissionId} deleted successfully.",
                    id);

                return ApiResponse<string>.Ok(
                    permission.Code,
                    "Permission deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error deleting permission {PermissionId}",
                    id);

                return ApiResponse<string>
                    .Fail("Unable to delete permission.");
            }
        }

        public async Task<ApiResponse<string>> RestoreAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(
                    "Restoring permission {PermissionId}",
                    id);

                var permission = await _unitOfWork.Permissions.GetDeletedPermissionAsync(id);

                if (permission == null)
                {
                    return ApiResponse<string>.Fail(
                        "Permission not found.");
                }

                if (!permission.IsDeleted)
                {
                    return ApiResponse<string>.Fail(
                        "Permission is already active.");
                }

                permission.IsDeleted = false;
                permission.ModifiedDate = DateTime.UtcNow;
                permission.ModifiedBy = "System";

                _unitOfWork.Permissions.Update(permission);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "Permission restored successfully.");

                return ApiResponse<string>.Ok(
                    permission.Code,
                    "Permission restored successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Restore permission failed.");

                return ApiResponse<string>.Fail(
                    "Unable to restore permission.");
            }
        }
        public async Task<ApiResponse<PagedPermissionResponseDto>> SearchAsync(SearchPermissionDto dto)
        {
            try
            {
                IQueryable<Permission> query = _unitOfWork.Permissions.Query();

                //-------------------------
                // Keyword Search
                //-------------------------

                if (!string.IsNullOrWhiteSpace(dto.Keyword))
                {
                    var keyword = dto.Keyword.Trim();

                    query = query.Where(x =>
                        EF.Functions.Like(x.Name, $"%{keyword}%") ||
                        EF.Functions.Like(x.Code, $"%{keyword}%"));
                }
                if (!string.IsNullOrWhiteSpace(dto.Code))
                {
                    var code = dto.Code.Trim();

                    query = query.Where(x =>
                    EF.Functions.Like(x.Code, $"{code}%"));
                }
                if (!string.IsNullOrWhiteSpace(dto.Name))
                {
                    var name = dto.Name.Trim();

                    query = query.Where(x =>
                    EF.Functions.Like(x.Name, $"{name}%"));
                }




                //-------------------------
                // Sorting
                //-------------------------

                query = dto.SortBy?.ToLower() switch
                {
                    "code" => dto.Descending
                        ? query.OrderByDescending(x => x.Code)
                        : query.OrderBy(x => x.Code),

                    "name" => dto.Descending
                        ? query.OrderByDescending(x => x.Name)
                        : query.OrderBy(x => x.Name),

                    _ => dto.Descending
                        ? query.OrderByDescending(x => x.Name)
                        : query.OrderBy(x => x.Name)
                };

                //-------------------------
                // Count
                //-------------------------

                var totalRecords = await query.CountAsync();

                //-------------------------
                // Paging
                //-------------------------

                dto.PageSize = Math.Min(dto.PageSize, 100);

                var permissions = await query
                    .Skip((dto.PageNumber - 1) * dto.PageSize)
                    .Take(dto.PageSize)
                    .ToListAsync();

                //-------------------------
                // Response
                //-------------------------

                var response = new PagedPermissionResponseDto
                {
                    Items = _mapper.Map<List<PermissionDto>>(permissions),
                    TotalRecords = totalRecords,
                    PageNumber = dto.PageNumber,
                    PageSize = dto.PageSize
                    //TotalPages = (int)Math.Ceiling(
                    //    totalRecords / (double)dto.PageSize)
                };

                return ApiResponse<PagedPermissionResponseDto>.Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Permission search failed.");

                return ApiResponse<PagedPermissionResponseDto>.Fail(
                    "Unable to search permissions.");
            }
        }

    }

}
