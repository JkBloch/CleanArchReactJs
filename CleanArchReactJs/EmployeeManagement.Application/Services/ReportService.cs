using AutoMapper;
using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.DTOs.Permissions;
using EmployeeManagement.Application.DTOs.Reports.Permissions;
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
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        
        }
        public async Task<ApiResponse<PermissionReportDto>> PermissionReportAsync(PermissionReportRequestDto dto)
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

                var response = new PermissionReportDto
                {
                    Items = _mapper.Map<List<PermissionDto>>(permissions),
                    TotalRecords = totalRecords,
                    PageNumber = dto.PageNumber,
                    PageSize = dto.PageSize
                    //TotalPages = (int)Math.Ceiling(
                    //    totalRecords / (double)dto.PageSize)
                };

                return ApiResponse<PermissionReportDto>.Ok(response);
            }
            catch (Exception ex)
            { 

                return ApiResponse<PermissionReportDto>.Fail(
                    "Unable to search permissions.");
            }
        }

        //public async Task<PagedResult<PermissionReportDto>> PermissionReportAsync(PermissionReportRequestDto request)
        //{
        //    var result = await _unitOfWork.Permissions.SearchAsync(
        //        request.Keyword
        //        //filter.Keyword,
        //        //filter.DepartmentId,
        //        //filter.IsActive,
        //        //filter.PageNumber,
        //        //filter.PageSize
        //        );

        //    return new PagedResult<PermissionReportDto>
        //    {
        //        Items = _mapper.Map<List<PermissionReportDto>>(result),

        //        TotalRecords = result.TotalRecords,

        //        PageNumber = filter.PageNumber,

        //        PageSize = filter.PageSize
        //    };
        //}

        //public async Task<PermissionReportSummaryDto> SummaryAsync(PermissionReportRequestDto request)
        //{
        //    return new PermissionReportSummaryDto
        //    {
        //        TotalPermissions =
        //            await _unitOfWork.Reports.TotalPermissionsAsync(),

        //        ActivePermissions =
        //            await _unitOfWork.Reports.ActivePermissionsAsync(),

        //        TotalSalary =
        //            await _unitOfWork.Reports.TotalSalaryAsync()
        //    };
        //}
    }
}
