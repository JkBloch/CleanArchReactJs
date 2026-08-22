using AutoMapper;
using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.Common.SearchExport.Master;
using EmployeeManagement.Application.DTOs.Master.Employee;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Application.Interfaces.Master;
using EmployeeManagement.Domain.Entities.Master;
using EmployeeManagement.Domain.Enums;
using EmployeeManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Services.Master
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeService> _logger;
        private readonly IRedisCacheService _redisCacheService;
        private readonly IFileStorageService _fileStorageService;
        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<EmployeeService> logger
            , IRedisCacheService redisCacheService, IFileStorageService fileStorageService)
        {   
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _redisCacheService = redisCacheService;
            _fileStorageService = fileStorageService;
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

                var cacheKey = CacheKeys.Employee(id);

                var cached = await _redisCacheService.GetAsync<EmployeeDto>(cacheKey);

                if (cached != null)
                {
                    return ApiResponse<EmployeeDto>
                   .Ok(cached);
                }

                var employee = await _unitOfWork.Employees.GetEmployeeByIdAsync(id);

                if (employee == null)
                {
                    return ApiResponse<EmployeeDto>
                        .Fail("Employee not found.");
                }

                var dto = _mapper.Map<EmployeeDto>(employee);

                await _redisCacheService.SetAsync(cacheKey, dto, TimeSpan.FromMinutes(10));

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

                await _redisCacheService.RemoveAsync(CacheKeys.Employee(employee.Id));
                await _redisCacheService.RemoveByPatternAsync(CacheKeys.EmployeeSearch("") + "*");
                await _redisCacheService.RemoveAsync(CacheKeys.DashboardStatistics());

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
                await _redisCacheService.RemoveAsync(CacheKeys.Employee(employee.Id));
                await _redisCacheService.RemoveByPatternAsync(CacheKeys.EmployeeSearch("") + "*");
                await _redisCacheService.RemoveAsync(CacheKeys.DashboardStatistics());
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
                await _redisCacheService.RemoveAsync(CacheKeys.Employee(employee.Id));
                await _redisCacheService.RemoveByPatternAsync(CacheKeys.EmployeeSearch("")+"*");
                await _redisCacheService.RemoveAsync(CacheKeys.DashboardStatistics());

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
                await _redisCacheService.RemoveAsync(CacheKeys.Employee(employee.Id));
                await _redisCacheService.RemoveAsync(CacheKeys.EmployeeSearch(""));
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
                _logger.LogInformation("Restoring employee {EmployeeId}",
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
                await _redisCacheService.RemoveAsync(CacheKeys.Employee(employee.Id));
                await _redisCacheService.RemoveAsync(CacheKeys.EmployeeSearch(""));
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
                var searchCachKey = dto.Keyword + dto.Name + dto.Code + dto.Email + dto.DepartmentId + dto.StateId
                     + dto.CityId + dto.SalaryFrom + dto.SalaryTo + dto.DateOfBirthFrom + dto.DateOfBirthTo 
                     + dto.JoiningDateFrom + dto.JoiningDateTo + dto.PageNumber + dto.PageSize
                     + dto.SortBy + dto.Descending;

                var cacheKey = CacheKeys.EmployeeSearch(searchCachKey);

                var cachedEmployees =
                    await _redisCacheService.GetAsync<PagedEmployeeResponseDto>(
                        cacheKey);


                if (cachedEmployees is not null)
                {

                    return ApiResponse<PagedEmployeeResponseDto>.Ok(cachedEmployees);

                }

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
                await _redisCacheService.SetAsync(
                   cacheKey, response,
                   TimeSpan.FromMinutes(10));
                return ApiResponse<PagedEmployeeResponseDto>.Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Employee search failed.");

                return ApiResponse<PagedEmployeeResponseDto>.Fail(
                    "Unable to search employees.");
            }
        }

        public async Task<ApiResponse<string>> UploadPhotoAsync(Guid employeeId, Stream stream, string fileName,
            string contentType, CancellationToken cancellationToken = default)
        {
            var employee =
                await _unitOfWork.Employees.GetByIdAsync(
                    employeeId,
                    cancellationToken);

            if (employee == null)
                return ApiResponse<string>.Fail( "Employee not found.");

            var allowedTypes = new[] { "image/jpeg", "image/png", "image/webp" };

            if (!allowedTypes.Contains(contentType.ToLowerInvariant()))
            {
                return ApiResponse<string>.Fail("Only JPG, PNG and WEBP images are allowed.");
            }

            var extension = Path.GetExtension(fileName).ToLowerInvariant();

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };

            if (!allowedExtensions.Contains(extension))
            {
                return ApiResponse<string>.Fail(
                    "Invalid image extension.");
            }

            if (stream.Length > 5 * 1024 * 1024)
            {
                return ApiResponse<string>.Fail(
                    "Maximum photo size is 5 MB.");
            }

            // Delete old photo
            if (!string.IsNullOrWhiteSpace(employee.PhotoUrl))
            {
                await _fileStorageService.DeleteAsync(
                    employee.PhotoUrl,
                    cancellationToken);
            }

            var upload =
                await _fileStorageService.UploadAsync(
                    stream,
                    fileName,
                    contentType,
                    "employees",
                    cancellationToken);

            employee.PhotoUrl = upload.Url;

            employee.PhotoFileName = upload.FileName;

            employee.ModifiedDate = DateTime.UtcNow;

            employee.ModifiedBy = "System";

            _unitOfWork.Employees.Update(employee);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _redisCacheService.RemoveAsync(CacheKeys.Employee(employee.Id));
            await _redisCacheService.RemoveAsync(CacheKeys.EmployeeSearch("*"));
            _logger.LogInformation(
                "Employee {EmployeeId} photo uploaded",
                employeeId);

            return ApiResponse<string>.Ok(
                upload.Url,
                "Photo uploaded successfully.");
        }
        public async Task<ApiResponse<string>> DeletePhotoAsync(Guid employeeId, CancellationToken cancellationToken = default)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(
                    employeeId,
                    cancellationToken);

            if (employee == null)
                return ApiResponse<string>.Fail(
                    "Employee not found.");

            if (string.IsNullOrWhiteSpace(
                employee.PhotoUrl))
            {
                return ApiResponse<string>.Fail(
                    "Employee does not have a photo.");
            }

            await _fileStorageService.DeleteAsync(
                employee.PhotoUrl,
                cancellationToken);

            employee.PhotoUrl = null;
            employee.PhotoFileName = null;

            employee.ModifiedDate =
                DateTime.UtcNow;

            employee.ModifiedBy = "System";

            _unitOfWork.Employees.Update(employee);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _redisCacheService.RemoveAsync(CacheKeys.Employee(employee.Id));
            await _redisCacheService.RemoveAsync(CacheKeys.EmployeeSearch(""));
            _logger.LogInformation(
                "Employee {EmployeeId} photo deleted",
                employeeId);

            return ApiResponse<string>.Ok(
                string.Empty,
                "Photo deleted successfully.");
        }



        #region DummyData

        public async Task<ApiResponse<string>> CreateDummyData(CancellationToken cancellationToken = default)
        {
            try
            {
                //var employee = _mapper.Map<Employee>(dto);
                var employees = await _unitOfWork.Employees.GetAllAsync();
                var departments = await _unitOfWork.Departments.GetAllAsync();
                //var states = await _unitOfWork.States.GetAllAsync();
                //var cities = await _unitOfWork.States.GetAllAsync();

                //var deparmentQuery = _unitOfWork.Departments.Query();
                //var departments = await deparmentQuery.AsNoTracking().ToListAsync();
                var stateQuery = _unitOfWork.States.Query();
                var states = await stateQuery.Include(x=>x.Cities).AsNoTracking().ToListAsync();
                for (int i = 0; i < 2; i++)
                {
                    foreach (var department in departments)
                    {
                        foreach (var state in states)
                        {
                            foreach (var city in state.Cities)
                            {
                                Employee employee = new Employee();
                                employee.Code = GenerateRandomCode();
                                employee.Name = GenerateRandomString();
                                employee.Email = GenerateRandomEmail();
                                employee.PhoneNumber = GenerateRandomPhoneNumber();
                                employee.DepartmentId = department.Id;
                                employee.StateId = state.Id;
                                employee.CityId = city.Id;
                                employee.Salary = GenerateRandomSalary();
                                employee.DateOfBirth = GenerateRandomDateOfBirth();
                                employee.JoiningDate = GenerateRandomJoiningDate();
                                employee.Gender = GetRandomGender();
                                employee.IsActive = Random.Shared.Next(2) == 0;
                                await _unitOfWork.Employees.AddAsync(employee, cancellationToken);
                               
                            }
                        }

                    }
                }
                await _unitOfWork.SaveChangesAsync();
                return ApiResponse<string>.Ok(  "Employee created successfully.");
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Fail(
                    "Unable to create employee.");
            }
        }
        public static string GenerateRandomString()
        {
            const string chars =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                "abcdefghijklmnopqrstuvwxyz" +
                "0123456789";

            int length = RandomNumberGenerator.GetInt32(8, 15);

            return string.Create(length, chars, (buffer, chars) =>
            {
                for (int i = 0; i < buffer.Length; i++)
                {
                    buffer[i] = chars[RandomNumberGenerator.GetInt32(chars.Length)];
                }
            });
        }
        public static string GenerateRandomCode()
        {
            const string chars =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                "0123456789";

            int length = RandomNumberGenerator.GetInt32(4, 6);

            var result= string.Create(length, chars, (buffer, chars) =>
            {
                for (int i = 0; i < buffer.Length; i++)
                {
                    buffer[i] = chars[RandomNumberGenerator.GetInt32(chars.Length)];
                }
            });

            return "E" + result;
        }
        public static string GenerateRandomEmail()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";

            int length = RandomNumberGenerator.GetInt32(8, 15);

            string username = string.Create(length, chars, (buffer, chars) =>
            {
                for (int i = 0; i < buffer.Length; i++)
                {
                    buffer[i] = chars[RandomNumberGenerator.GetInt32(chars.Length)];
                }
            });
            if (length>10)
            {
                return $"{username}@gmail.com";
            }
            else
            {
                return $"{username}@yahoo.com";
            }
                
        }
        public static string GenerateRandomPhoneNumber()
        {
            int firstDigit = RandomNumberGenerator.GetInt32(6, 10);

            int remaining = RandomNumberGenerator.GetInt32(0, 1_000_000_000);

            return $"{firstDigit}{remaining:D9}";
        }
  
        public static decimal GenerateRandomSalary() 
        { 
            int salary = RandomNumberGenerator.GetInt32(20_000, 100_001); 
            return salary; 
        }
        public static DateTime GenerateRandomDateOfBirth()
        {
            DateTime today = DateTime.Today;

            DateTime minDate = today.AddYears(-60);
            DateTime maxDate = today.AddYears(-18);

            int days = (maxDate - minDate).Days;

            return minDate.AddDays(
                RandomNumberGenerator.GetInt32(days + 1));
        }

        public static DateTime GenerateRandomJoiningDate()
        {
            DateTime startDate = new DateTime(2015, 1, 1);
            DateTime endDate = DateTime.Today;

            int days = (endDate - startDate).Days;

            return startDate.AddDays(
                RandomNumberGenerator.GetInt32(days + 1));
        }
        private static Gender GetRandomGender()
        {
            string[] genders =
            { 
                "Male", 
                "Female" 
            };

            var genderString= genders[Random.Shared.Next(genders.Length)];

            if (genderString == " Male")
            {
                return Gender.Male;
            }
            else if (genderString == " Female")
            {
                return Gender.Female;
            }            
            else
            {
                return Gender.Male;
            }

        }
        #endregion
    }

}
