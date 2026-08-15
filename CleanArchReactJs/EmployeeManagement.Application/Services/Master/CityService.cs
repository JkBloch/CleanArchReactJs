using AutoMapper;
using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.Common.SearchExport.Master;
using EmployeeManagement.Application.DTOs.Master.City;
using EmployeeManagement.Application.Interfaces.Master;
using EmployeeManagement.Domain.Entities.Master;
using EmployeeManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Services.Master
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CityService> _logger;
        public CityService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CityService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ApiResponse<IEnumerable<CityDto>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Loading all cities.");

                var cities =
                    await _unitOfWork.Cities.GetAllAsync();

                var result =
                    _mapper.Map<IEnumerable<CityDto>>(cities);

                return ApiResponse<IEnumerable<CityDto>>
                    .Ok(result, "Cities loaded successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while loading cities.");

                return ApiResponse<IEnumerable<CityDto>>
                    .Fail("Unable to load cities.");
            }
        }
        public async Task<ApiResponse<CityDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    "Loading city {CityId}", id);

                var city = await _unitOfWork.Cities.GetCityByIdAsync(id, cancellationToken);

                if (city == null)
                {
                    return ApiResponse<CityDto>
                        .Fail("City not found.");
                }

                var dto = _mapper.Map<CityDto>(city);

                return ApiResponse<CityDto>
                    .Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error loading city {CityId}",
                    id);

                return ApiResponse<CityDto>
                    .Fail("Unable to load city.");
            }
        }
        public async Task<ApiResponse<string>> CreateAsync(CreateCityDto dto, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    "Creating city {CityCode}",
                    dto.Code);

                // Email validation
                var nameExists = await _unitOfWork.Cities.GetByNameAsync(dto.Name, cancellationToken);

                if (nameExists != null && nameExists.Id != Guid.Empty)
                {
                    return ApiResponse<string>.Fail(
                        "Name already exists.");
                }

                // City Code validation
                var codeExists = await _unitOfWork.Cities.GetByCodeAsync(dto.Code, cancellationToken);

                if (codeExists != null && codeExists.Id != Guid.Empty)
                {
                    return ApiResponse<string>.Fail(
                        "Code already exists.");
                }


                var city = _mapper.Map<City>(dto);

                await _unitOfWork.Cities.AddAsync(city, cancellationToken);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "City {CityCode} created successfully.",
                    dto.Code);

                return ApiResponse<string>.Ok(
                    city.Code,
                    "City created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error creating city.");

                return ApiResponse<string>.Fail(
                    "Unable to create city.");
            }
        }
        public async Task<ApiResponse<string>> UpdateAsync(UpdateCityDto dto, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    "Updating city {CityId}",
                    dto.Id);

                var city = await _unitOfWork.Cities
                        .GetByIdAsync(dto.Id, cancellationToken);

                if (city == null)
                {
                    return ApiResponse<string>
                        .Fail("City not found.");
                }

                if (await _unitOfWork.Cities.NameExistsAsync(dto.Name, dto.Id, cancellationToken))
                {
                    return ApiResponse<string>
                        .Fail("Name already exists.");
                }

                if (await _unitOfWork.Cities.CodeExistsAsync(dto.Code, dto.Id, cancellationToken))
                {
                    return ApiResponse<string>
                        .Fail("Code already exists.");
                }

                _mapper.Map(dto, city);

                city.ModifiedDate = DateTime.UtcNow;
                city.ModifiedBy = "System";

                _unitOfWork.Cities.Update(city);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "City {CityId} updated successfully.",
                    city.Id);

                return ApiResponse<string>.Ok(
                    city.Code,
                    "City updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error updating city {CityId}",
                    dto.Id);

                return ApiResponse<string>
                    .Fail("Unable to update city.");
            }
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    "Deleting city {CityId}",
                    id);

                var city = await _unitOfWork.Cities.GetByIdAsync(id, cancellationToken);

                if (city == null)
                {
                    return ApiResponse<string>
                        .Fail("City not found.");
                }

                city.IsDeleted = true;
                city.ModifiedDate = DateTime.UtcNow;
                city.ModifiedBy = "System";

                _unitOfWork.Cities.Update(city);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "City {CityId} deleted successfully.",
                    id);

                return ApiResponse<string>.Ok(
                    city.Code,
                    "City deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error deleting city {CityId}",
                    id);

                return ApiResponse<string>
                    .Fail("Unable to delete city.");
            }
        }
        public async Task<ApiResponse<string>> DeletePermanentAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    "Deleting city {CityId}",
                    id);

                var city = await _unitOfWork.Cities.GetDeletedCityAsync(id, cancellationToken);

                if (city == null)
                {
                    return ApiResponse<string>
                        .Fail("City not found.");
                }

                _unitOfWork.Cities.Delete(city);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "City {CityId} deleted successfully.",
                    id);

                return ApiResponse<string>.Ok(
                    city.Code,
                    "City deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error deleting city {CityId}",
                    id);

                return ApiResponse<string>
                    .Fail("Unable to delete city.");
            }
        }

        public async Task<ApiResponse<string>> RestoreAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    "Restoring city {CityId}",
                    id);

                var city = await _unitOfWork.Cities.GetDeletedCityAsync(id, cancellationToken);

                if (city == null)
                {
                    return ApiResponse<string>.Fail(
                        "City not found.");
                }

                if (!city.IsDeleted)
                {
                    return ApiResponse<string>.Fail(
                        "City is already active.");
                }

                city.IsDeleted = false;
                city.ModifiedDate = DateTime.UtcNow;
                city.ModifiedBy = "System";

                _unitOfWork.Cities.Update(city);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "City restored successfully.");

                return ApiResponse<string>.Ok(
                    city.Code,
                    "City restored successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Restore city failed.");

                return ApiResponse<string>.Fail(
                    "Unable to restore city.");
            }
        }
        public async Task<ApiResponse<PagedCityResponseDto>> SearchAsync(SearchCityDto dto,
            CancellationToken cancellationToken = default)
        {
            try
            {
                IQueryable<City> query = _unitOfWork.Cities.Query().Include(x=>x.State);

                var (cities, totalRecords) = await CitySearchData.GetExportCityData(query, dto, "page", cancellationToken);

                //-------------------------
                // Response
                //-------------------------

                var response = new PagedCityResponseDto
                {
                    Items = _mapper.Map<List<CityDto>>(cities),
                    TotalRecords = totalRecords,
                    PageNumber = dto.PageNumber,
                    PageSize = dto.PageSize
                    //TotalPages = (int)Math.Ceiling(
                    //    totalRecords / (double)dto.PageSize)
                };

                return ApiResponse<PagedCityResponseDto>.Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "City search failed.");

                return ApiResponse<PagedCityResponseDto>.Fail(
                    "Unable to search cities.");
            }
        }

    }

}
