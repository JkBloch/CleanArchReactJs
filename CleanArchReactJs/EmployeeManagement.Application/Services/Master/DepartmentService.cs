using AutoMapper;
using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.Common.SearchExport.Master;
using EmployeeManagement.Application.DTOs.Master.Department;
using EmployeeManagement.Application.Interfaces.Master;
using EmployeeManagement.Domain.Entities.Master;
using EmployeeManagement.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Services.Master
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentService> _logger;
        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DepartmentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ApiResponse<IEnumerable<DepartmentDto>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Loading all departments.");

                var departments =
                    await _unitOfWork.Departments.GetAllAsync();

                var result =
                    _mapper.Map<IEnumerable<DepartmentDto>>(departments);

                return ApiResponse<IEnumerable<DepartmentDto>>
                    .Ok(result, "Departments loaded successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while loading departments.");

                return ApiResponse<IEnumerable<DepartmentDto>>
                    .Fail("Unable to load departments.");
            }
        }
        public async Task<ApiResponse<DepartmentDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    "Loading department {DepartmentId}", id);

                var department = await _unitOfWork.Departments.GetByIdAsync(id);

                if (department == null)
                {
                    return ApiResponse<DepartmentDto>
                        .Fail("Department not found.");
                }

                var dto = _mapper.Map<DepartmentDto>(department);

                return ApiResponse<DepartmentDto>
                    .Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error loading department {DepartmentId}",
                    id);

                return ApiResponse<DepartmentDto>
                    .Fail("Unable to load department.");
            }
        }
        public async Task<ApiResponse<string>> CreateAsync(CreateDepartmentDto dto, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    "Creating department {DepartmentCode}",
                    dto.Code);

                // Email validation
                var nameExists = await _unitOfWork.Departments.GetByNameAsync(dto.Name, cancellationToken);

                if (nameExists != null && nameExists.Id != Guid.Empty)
                {
                    return ApiResponse<string>.Fail(
                        "Name already exists.");
                }

                // Department Code validation
                var codeExists = await _unitOfWork.Departments.GetByCodeAsync(dto.Code, cancellationToken);

                if (codeExists != null && codeExists.Id != Guid.Empty)
                {
                    return ApiResponse<string>.Fail(
                        "Code already exists.");
                }


                var department = _mapper.Map<Department>(dto);

                await _unitOfWork.Departments.AddAsync(department, cancellationToken);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "Department {DepartmentCode} created successfully.",
                    dto.Code);

                return ApiResponse<string>.Ok(
                    department.Code,
                    "Department created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error creating department.");

                return ApiResponse<string>.Fail(
                    "Unable to create department.");
            }
        }
        public async Task<ApiResponse<string>> UpdateAsync(UpdateDepartmentDto dto, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    "Updating department {DepartmentId}",
                    dto.Id);

                var department = await _unitOfWork.Departments
                        .GetByIdAsync(dto.Id, cancellationToken);

                if (department == null)
                {
                    return ApiResponse<string>
                        .Fail("Department not found.");
                }

                if (await _unitOfWork.Departments.NameExistsAsync(dto.Name, dto.Id, cancellationToken))
                {
                    return ApiResponse<string>
                        .Fail("Name already exists.");
                }

                if (await _unitOfWork.Departments.CodeExistsAsync(dto.Code, dto.Id, cancellationToken))
                {
                    return ApiResponse<string>
                        .Fail("Code already exists.");
                }

                _mapper.Map(dto, department);

                department.ModifiedDate = DateTime.UtcNow;
                department.ModifiedBy = "System";

                _unitOfWork.Departments.Update(department);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "Department {DepartmentId} updated successfully.",
                    department.Id);

                return ApiResponse<string>.Ok(
                    department.Code,
                    "Department updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error updating department {DepartmentId}",
                    dto.Id);

                return ApiResponse<string>
                    .Fail("Unable to update department.");
            }
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    "Deleting department {DepartmentId}",
                    id);

                var department = await _unitOfWork.Departments.GetByIdAsync(id, cancellationToken);

                if (department == null)
                {
                    return ApiResponse<string>
                        .Fail("Department not found.");
                }

                department.IsDeleted = true;
                department.ModifiedDate = DateTime.UtcNow;
                department.ModifiedBy = "System";

                _unitOfWork.Departments.Update(department);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "Department {DepartmentId} deleted successfully.",
                    id);

                return ApiResponse<string>.Ok(
                    department.Code,
                    "Department deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error deleting department {DepartmentId}",
                    id);

                return ApiResponse<string>
                    .Fail("Unable to delete department.");
            }
        }
        public async Task<ApiResponse<string>> DeletePermanentAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    "Deleting department {DepartmentId}",
                    id);

                var department = await _unitOfWork.Departments.GetDeletedDepartmentAsync(id, cancellationToken);

                if (department == null)
                {
                    return ApiResponse<string>
                        .Fail("Department not found.");
                }

                _unitOfWork.Departments.Delete(department);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "Department {DepartmentId} deleted successfully.",
                    id);

                return ApiResponse<string>.Ok(
                    department.Code,
                    "Department deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error deleting department {DepartmentId}",
                    id);

                return ApiResponse<string>
                    .Fail("Unable to delete department.");
            }
        }

        public async Task<ApiResponse<string>> RestoreAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    "Restoring department {DepartmentId}",
                    id);

                var department = await _unitOfWork.Departments.GetDeletedDepartmentAsync(id, cancellationToken);

                if (department == null)
                {
                    return ApiResponse<string>.Fail(
                        "Department not found.");
                }

                if (!department.IsDeleted)
                {
                    return ApiResponse<string>.Fail(
                        "Department is already active.");
                }

                department.IsDeleted = false;
                department.ModifiedDate = DateTime.UtcNow;
                department.ModifiedBy = "System";

                _unitOfWork.Departments.Update(department);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "Department restored successfully.");

                return ApiResponse<string>.Ok(
                    department.Code,
                    "Department restored successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Restore department failed.");

                return ApiResponse<string>.Fail(
                    "Unable to restore department.");
            }
        }
        public async Task<ApiResponse<PagedDepartmentResponseDto>> SearchAsync(SearchDepartmentDto dto,
            CancellationToken cancellationToken = default)
        {
            try
            {
                IQueryable<Department> query = _unitOfWork.Departments.Query();

                var (departments, totalRecords) = await DepartmentSearchData.GetExportDepartmentData(query, dto, "page", cancellationToken);

                //-------------------------
                // Response
                //-------------------------

                var response = new PagedDepartmentResponseDto
                {
                    Items = _mapper.Map<List<DepartmentDto>>(departments),
                    TotalRecords = totalRecords,
                    PageNumber = dto.PageNumber,
                    PageSize = dto.PageSize
                    //TotalPages = (int)Math.Ceiling(
                    //    totalRecords / (double)dto.PageSize)
                };

                return ApiResponse<PagedDepartmentResponseDto>.Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Department search failed.");

                return ApiResponse<PagedDepartmentResponseDto>.Fail(
                    "Unable to search departments.");
            }
        }

    }
}
