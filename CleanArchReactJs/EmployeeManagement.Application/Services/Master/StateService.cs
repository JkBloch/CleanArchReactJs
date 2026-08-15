using AutoMapper;
using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.DTOs.Master.State;
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
    public class StateService : IStateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<StateService> _logger;
        public StateService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<StateService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ApiResponse<IEnumerable<StateDto>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Loading all states.");

                var states =
                    await _unitOfWork.States.GetAllAsync();

                var result =
                    _mapper.Map<IEnumerable<StateDto>>(states);

                return ApiResponse<IEnumerable<StateDto>>
                    .Ok(result, "States loaded successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while loading states.");

                return ApiResponse<IEnumerable<StateDto>>
                    .Fail("Unable to load states.");
            }
        }
        public async Task<ApiResponse<StateDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    "Loading state {StateId}", id);

                var state = await _unitOfWork.States.GetByIdAsync(id);

                if (state == null)
                {
                    return ApiResponse<StateDto>
                        .Fail("State not found.");
                }

                var dto = _mapper.Map<StateDto>(state);

                return ApiResponse<StateDto>
                    .Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error loading state {StateId}",
                    id);

                return ApiResponse<StateDto>
                    .Fail("Unable to load state.");
            }
        }
        public async Task<ApiResponse<string>> CreateAsync(CreateStateDto dto, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    "Creating state {StateCode}",
                    dto.Code);

                // Email validation
                var nameExists = await _unitOfWork.States.GetByNameAsync(dto.Name, cancellationToken);

                if (nameExists != null && nameExists.Id != Guid.Empty)
                {
                    return ApiResponse<string>.Fail(
                        "Name already exists.");
                }

                // State Code validation
                var codeExists = await _unitOfWork.States.GetByCodeAsync(dto.Code, cancellationToken);

                if (codeExists != null && codeExists.Id != Guid.Empty)
                {
                    return ApiResponse<string>.Fail(
                        "Code already exists.");
                }


                var state = _mapper.Map<State>(dto);

                await _unitOfWork.States.AddAsync(state, cancellationToken);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "State {StateCode} created successfully.",
                    dto.Code);

                return ApiResponse<string>.Ok(
                    state.Code,
                    "State created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error creating state.");

                return ApiResponse<string>.Fail(
                    "Unable to create state.");
            }
        }
        public async Task<ApiResponse<string>> UpdateAsync(UpdateStateDto dto, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    "Updating state {StateId}",
                    dto.Id);

                var state = await _unitOfWork.States
                        .GetByIdAsync(dto.Id,cancellationToken);

                if (state == null)
                {
                    return ApiResponse<string>
                        .Fail("State not found.");
                }

                if (await _unitOfWork.States.NameExistsAsync(dto.Name, dto.Id, cancellationToken))
                {
                    return ApiResponse<string>
                        .Fail("Name already exists.");
                }

                if (await _unitOfWork.States.CodeExistsAsync(dto.Code, dto.Id, cancellationToken))
                {
                    return ApiResponse<string>
                        .Fail("Code already exists.");
                }

                _mapper.Map(dto, state);

                state.ModifiedDate = DateTime.UtcNow;
                state.ModifiedBy = "System";

                _unitOfWork.States.Update(state);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "State {StateId} updated successfully.",
                    state.Id);

                return ApiResponse<string>.Ok(
                    state.Code,
                    "State updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error updating state {StateId}",
                    dto.Id);

                return ApiResponse<string>
                    .Fail("Unable to update state.");
            }
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    "Deleting state {StateId}",
                    id);

                var state = await _unitOfWork.States.GetByIdAsync(id,cancellationToken);

                if (state == null)
                {
                    return ApiResponse<string>
                        .Fail("State not found.");
                }

                state.IsDeleted = true;
                state.ModifiedDate = DateTime.UtcNow;
                state.ModifiedBy = "System";

                _unitOfWork.States.Update(state);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "State {StateId} deleted successfully.",
                    id);

                return ApiResponse<string>.Ok(
                    state.Code,
                    "State deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error deleting state {StateId}",
                    id);

                return ApiResponse<string>
                    .Fail("Unable to delete state.");
            }
        }
        public async Task<ApiResponse<string>> DeletePermanentAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    "Deleting state {StateId}",
                    id);

                var state = await _unitOfWork.States.GetDeletedStateAsync(id, cancellationToken);

                if (state == null)
                {
                    return ApiResponse<string>
                        .Fail("State not found.");
                }

                _unitOfWork.States.Delete(state);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "State {StateId} deleted successfully.",
                    id);

                return ApiResponse<string>.Ok(
                    state.Code,
                    "State deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error deleting state {StateId}",
                    id);

                return ApiResponse<string>
                    .Fail("Unable to delete state.");
            }
        }

        public async Task<ApiResponse<string>> RestoreAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    "Restoring state {StateId}",
                    id);

                var state = await _unitOfWork.States.GetDeletedStateAsync(id, cancellationToken);

                if (state == null)
                {
                    return ApiResponse<string>.Fail(
                        "State not found.");
                }

                if (!state.IsDeleted)
                {
                    return ApiResponse<string>.Fail(
                        "State is already active.");
                }

                state.IsDeleted = false;
                state.ModifiedDate = DateTime.UtcNow;
                state.ModifiedBy = "System";

                _unitOfWork.States.Update(state);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "State restored successfully.");

                return ApiResponse<string>.Ok(
                    state.Code,
                    "State restored successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Restore state failed.");

                return ApiResponse<string>.Fail(
                    "Unable to restore state.");
            }
        }
        public async Task<ApiResponse<PagedStateResponseDto>> SearchAsync(SearchStateDto dto, 
            CancellationToken cancellationToken = default)
        {
            try
            {
                IQueryable<State> query = _unitOfWork.States.Query();

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

                var totalRecords = await query.CountAsync(cancellationToken);

                //-------------------------
                // Paging
                //-------------------------

                dto.PageSize = Math.Min(dto.PageSize, 100);

                var states = await query
                    .Skip((dto.PageNumber - 1) * dto.PageSize)
                    .Take(dto.PageSize)
                    .ToListAsync(cancellationToken);

                //-------------------------
                // Response
                //-------------------------

                var response = new PagedStateResponseDto
                {
                    Items = _mapper.Map<List<StateDto>>(states),
                    TotalRecords = totalRecords,
                    PageNumber = dto.PageNumber,
                    PageSize = dto.PageSize
                    //TotalPages = (int)Math.Ceiling(
                    //    totalRecords / (double)dto.PageSize)
                };

                return ApiResponse<PagedStateResponseDto>.Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "State search failed.");

                return ApiResponse<PagedStateResponseDto>.Fail(
                    "Unable to search states.");
            }
        }

    }
}
