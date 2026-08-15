using AutoMapper;
using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.Common.SearchExport.Admin;
using EmployeeManagement.Application.DTOs.Admin.Roles;
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
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<RoleService> _logger;
        public RoleService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<RoleService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ApiResponse<IEnumerable<RoleDto>>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Loading all roles.");

                var roles =
                    await _unitOfWork.Roles.GetAllAsync();

                var result =
                    _mapper.Map<IEnumerable<RoleDto>>(roles);

                return ApiResponse<IEnumerable<RoleDto>>
                    .Ok(result, "Roles loaded successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while loading roles.");

                return ApiResponse<IEnumerable<RoleDto>>
                    .Fail("Unable to load roles.");
            }
        }
        public async Task<ApiResponse<RoleDto>> GetByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(
                    "Loading role {RoleId}", id);

                var role = await _unitOfWork.Roles.GetByIdAsync(id);

                if (role == null)
                {
                    return ApiResponse<RoleDto>
                        .Fail("Role not found.");
                }

                var dto = _mapper.Map<RoleDto>(role);

                return ApiResponse<RoleDto>
                    .Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error loading role {RoleId}",
                    id);

                return ApiResponse<RoleDto>
                    .Fail("Unable to load role.");
            }
        }
        public async Task<ApiResponse<string>> CreateAsync(CreateRoleDto dto)
        {
            try
            {
                _logger.LogInformation(
                    "Creating role {RoleCode}",
                    dto.Code);

                // Email validation
                var nameExists = await _unitOfWork.Roles.GetByNameAsync(dto.Name);

                if (nameExists != null && nameExists.Id != Guid.Empty)
                {
                    return ApiResponse<string>.Fail(
                        "Name already exists.");
                }

                // Role Code validation
                var codeExists = await _unitOfWork.Roles.GetByCodeAsync(dto.Code);

                if (codeExists != null && codeExists.Id != Guid.Empty)
                {
                    return ApiResponse<string>.Fail(
                        "Code already exists.");
                }


                var role = _mapper.Map<Role>(dto);

                await _unitOfWork.Roles.AddAsync(role);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "Role {RoleCode} created successfully.",
                    dto.Code);

                return ApiResponse<string>.Ok(
                    role.Code,
                    "Role created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error creating role.");

                return ApiResponse<string>.Fail(
                    "Unable to create role.");
            }
        }
        public async Task<ApiResponse<string>> UpdateAsync(UpdateRoleDto dto)
        {
            try
            {
                _logger.LogInformation(
                    "Updating role {RoleId}",
                    dto.Id);

                var role = await _unitOfWork.Roles
                        .GetByIdAsync(dto.Id);

                if (role == null)
                {
                    return ApiResponse<string>
                        .Fail("Role not found.");
                }

                if (await _unitOfWork.Roles.NameExistsAsync(dto.Name, dto.Id))
                {
                    return ApiResponse<string>
                        .Fail("Name already exists.");
                }

                if (await _unitOfWork.Roles.CodeExistsAsync(dto.Code, dto.Id))
                {
                    return ApiResponse<string>
                        .Fail("Code already exists.");
                }

                _mapper.Map(dto, role);

                role.ModifiedDate = DateTime.UtcNow;
                role.ModifiedBy = "System";

                _unitOfWork.Roles.Update(role);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "Role {RoleId} updated successfully.",
                    role.Id);

                return ApiResponse<string>.Ok(
                    role.Code,
                    "Role updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error updating role {RoleId}",
                    dto.Id);

                return ApiResponse<string>
                    .Fail("Unable to update role.");
            }
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(
                    "Deleting role {RoleId}",
                    id);

                var role = await _unitOfWork.Roles.GetByIdAsync(id);

                if (role == null)
                {
                    return ApiResponse<string>
                        .Fail("Role not found.");
                }

                role.IsDeleted = true;
                role.ModifiedDate = DateTime.UtcNow;
                role.ModifiedBy = "System";

                _unitOfWork.Roles.Update(role);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "Role {RoleId} deleted successfully.",
                    id);

                return ApiResponse<string>.Ok(
                    role.Code,
                    "Role deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error deleting role {RoleId}",
                    id);

                return ApiResponse<string>
                    .Fail("Unable to delete role.");
            }
        }
        public async Task<ApiResponse<string>> DeletePermanentAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(
                    "Deleting role {RoleId}",
                    id);

                var role = await _unitOfWork.Roles.GetByIdAsync(id);

                if (role == null)
                {
                    return ApiResponse<string>
                        .Fail("Role not found.");
                }

                _unitOfWork.Roles.Delete(role);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "Role {RoleId} deleted successfully.",
                    id);

                return ApiResponse<string>.Ok(
                    role.Code,
                    "Role deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error deleting role {RoleId}",
                    id);

                return ApiResponse<string>
                    .Fail("Unable to delete role.");
            }
        }

        public async Task<ApiResponse<string>> RestoreAsync(Guid id)
        {
            try
            {
                _logger.LogInformation(
                    "Restoring role {RoleId}",
                    id);

                var role = await _unitOfWork.Roles.GetDeletedRoleAsync(id);

                if (role == null)
                {
                    return ApiResponse<string>.Fail(
                        "Role not found.");
                }

                if (!role.IsDeleted)
                {
                    return ApiResponse<string>.Fail(
                        "Role is already active.");
                }

                role.IsDeleted = false;
                role.ModifiedDate = DateTime.UtcNow;
                role.ModifiedBy = "System";

                _unitOfWork.Roles.Update(role);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "Role restored successfully.");

                return ApiResponse<string>.Ok(
                    role.Code,
                    "Role restored successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Restore role failed.");

                return ApiResponse<string>.Fail(
                    "Unable to restore role.");
            }
        }
        public async Task<ApiResponse<PagedRoleResponseDto>> SearchAsync(SearchRoleDto dto)
        {
            try
            {
                IQueryable<Role> query = _unitOfWork.Roles.Query();

                var (roles, totalRecords) = await RoleSearchData.GetExportRoleData(query, dto, "page");

                //-------------------------
                // Response
                //-------------------------

                var response = new PagedRoleResponseDto
                {
                    Items = _mapper.Map<List<RoleDto>>(roles),
                    TotalRecords = totalRecords,
                    PageNumber = dto.PageNumber,
                    PageSize = dto.PageSize
                    //TotalPages = (int)Math.Ceiling(
                    //    totalRecords / (double)dto.PageSize)
                };

                return ApiResponse<PagedRoleResponseDto>.Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Role search failed.");

                return ApiResponse<PagedRoleResponseDto>.Fail(
                    "Unable to search roles.");
            }
        }

    }
}
