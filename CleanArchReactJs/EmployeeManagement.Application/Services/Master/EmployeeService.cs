using AutoMapper;
using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.Common.SearchExport.Master;
using EmployeeManagement.Application.DTOs.Master.Employee;
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
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeService> _logger;
        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<EmployeeService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ApiResponse<IEnumerable<EmployeeDto>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Loading all employees.");

                var employees =
                    await _unitOfWork.Employees.GetAllAsync();

                var result =
                    _mapper.Map<IEnumerable<EmployeeDto>>(employees);

                return ApiResponse<IEnumerable<EmployeeDto>>
                    .Ok(result, "Employees loaded successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while loading employees.");

                return ApiResponse<IEnumerable<EmployeeDto>>
                    .Fail("Unable to load employees.");
            }
        }
        public async Task<ApiResponse<EmployeeDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    "Loading employee {EmployeeId}", id);

                var employee = await _unitOfWork.Employees.GetEmployeeByIdAsync(id);

                if (employee == null)
                {
                    return ApiResponse<EmployeeDto>
                        .Fail("Employee not found.");
                }

                var dto = _mapper.Map<EmployeeDto>(employee);

                return ApiResponse<EmployeeDto>
                    .Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error loading employee {EmployeeId}",
                    id);

                return ApiResponse<EmployeeDto>
                    .Fail("Unable to load employee.");
            }
        }
        public async Task<ApiResponse<string>> CreateAsync(CreateEmployeeDto dto, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    "Creating employee {EmployeeCode}",
                    dto.Code);

                // Email validation
                var nameExists = await _unitOfWork.Employees.GetByNameAsync(dto.Name, cancellationToken);

                if (nameExists != null && nameExists.Id != Guid.Empty)
                {
                    return ApiResponse<string>.Fail(
                        "Name already exists.");
                }

                // Employee Code validation
                var codeExists = await _unitOfWork.Employees.GetByCodeAsync(dto.Code, cancellationToken);

                if (codeExists != null && codeExists.Id != Guid.Empty)
                {
                    return ApiResponse<string>.Fail(
                        "Code already exists.");
                }
                // Email validation
                var emailExists = await _unitOfWork.Employees.GetByEmailAsync(dto.Email, cancellationToken);

                if (emailExists != null && emailExists.Id != Guid.Empty)
                {
                    return ApiResponse<string>.Fail(
                        "Email already exists.");
                }


                var employee = _mapper.Map<Employee>(dto);

                await _unitOfWork.Employees.AddAsync(employee, cancellationToken);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "Employee {EmployeeCode} created successfully.",
                    dto.Code);

                return ApiResponse<string>.Ok(
                    employee.Code,
                    "Employee created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error creating employee.");

                return ApiResponse<string>.Fail(
                    "Unable to create employee.");
            }
        }
        public async Task<ApiResponse<string>> UpdateAsync(UpdateEmployeeDto dto, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    "Updating employee {EmployeeId}",
                    dto.Id);

                var employee = await _unitOfWork.Employees
                        .GetByIdAsync(dto.Id, cancellationToken);

                if (employee == null)
                {
                    return ApiResponse<string>
                        .Fail("Employee not found.");
                }

                if (await _unitOfWork.Employees.NameExistsAsync(dto.Name, dto.Id, cancellationToken))
                {
                    return ApiResponse<string>
                        .Fail("Name already exists.");
                }

                if (await _unitOfWork.Employees.CodeExistsAsync(dto.Code, dto.Id, cancellationToken))
                {
                    return ApiResponse<string>
                        .Fail("Code already exists.");
                }
                if (await _unitOfWork.Employees.EmailExistsAsync(dto.Email, dto.Id, cancellationToken))
                {
                    return ApiResponse<string>
                        .Fail("Code already exists.");
                }

                _mapper.Map(dto, employee);

                employee.ModifiedDate = DateTime.UtcNow;
                employee.ModifiedBy = "System";

                _unitOfWork.Employees.Update(employee);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "Employee {EmployeeId} updated successfully.",
                    employee.Id);

                return ApiResponse<string>.Ok(
                    employee.Code,
                    "Employee updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error updating employee {EmployeeId}",
                    dto.Id);

                return ApiResponse<string>
                    .Fail("Unable to update employee.");
            }
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogWarning(
                    "Deleting employee {EmployeeId}",
                    id);

                var employee = await _unitOfWork.Employees.GetByIdAsync(id, cancellationToken);

                if (employee == null)
                {
                    return ApiResponse<string>
                        .Fail("Employee not found.");
                }

                employee.IsDeleted = true;
                employee.ModifiedDate = DateTime.UtcNow;
                employee.ModifiedBy = "System";

                _unitOfWork.Employees.Update(employee);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogWarning(
                    "Employee {EmployeeId} deleted successfully.",
                    id);

                return ApiResponse<string>.Ok(
                    employee.Code,
                    "Employee deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error deleting employee {EmployeeId}",
                    id);

                return ApiResponse<string>
                    .Fail("Unable to delete employee.");
            }
        }
        public async Task<ApiResponse<string>> DeletePermanentAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogWarning(
                    "Deleting employee {EmployeeId}",
                    id);

                var employee = await _unitOfWork.Employees.GetDeletedEmployeeAsync(id, cancellationToken);

                if (employee == null)
                {
                    return ApiResponse<string>
                        .Fail("Employee not found.");
                }

                _unitOfWork.Employees.Delete(employee);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogWarning(
                    "Employee {EmployeeId} deleted successfully.",
                    id);

                return ApiResponse<string>.Ok(
                    employee.Code,
                    "Employee deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error deleting employee {EmployeeId}",
                    id);

                return ApiResponse<string>
                    .Fail("Unable to delete employee.");
            }
        }

        public async Task<ApiResponse<string>> RestoreAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    "Restoring employee {EmployeeId}",
                    id);

                var employee = await _unitOfWork.Employees.GetDeletedEmployeeAsync(id, cancellationToken);

                if (employee == null)
                {
                    return ApiResponse<string>.Fail(
                        "Employee not found.");
                }

                if (!employee.IsDeleted)
                {
                    return ApiResponse<string>.Fail(
                        "Employee is already active.");
                }

                employee.IsDeleted = false;
                employee.ModifiedDate = DateTime.UtcNow;
                employee.ModifiedBy = "System";

                _unitOfWork.Employees.Update(employee);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "Employee restored successfully.");

                return ApiResponse<string>.Ok(
                    employee.Code,
                    "Employee restored successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Restore employee failed.");

                return ApiResponse<string>.Fail(
                    "Unable to restore employee.");
            }
        }
        public async Task<ApiResponse<PagedEmployeeResponseDto>> SearchAsync(SearchEmployeeDto dto,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation( "Searching employees. Keyword: {Keyword}, Page: {PageNumber}, PageSize: {PageSize}", 
                    dto.Keyword, 
                    dto.PageNumber, 
                    dto.PageSize);
                IQueryable<Employee> query = _unitOfWork.Employees.Query();

                var (employees, totalRecords) = await EmployeeSearchData.GetExportEmployeeData(query, dto, "page", cancellationToken);

                //-------------------------
                // Response
                //-------------------------

                var response = new PagedEmployeeResponseDto
                {
                    Items = _mapper.Map<List<EmployeeDto>>(employees),
                    TotalRecords = totalRecords,
                    PageNumber = dto.PageNumber,
                    PageSize = dto.PageSize
                    //TotalPages = (int)Math.Ceiling(
                    //    totalRecords / (double)dto.PageSize)
                };

                return ApiResponse<PagedEmployeeResponseDto>.Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Employee search failed.");

                return ApiResponse<PagedEmployeeResponseDto>.Fail(
                    "Unable to search employees.");
            }
        }

    }

}
