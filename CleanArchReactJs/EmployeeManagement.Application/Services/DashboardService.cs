using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.DTOs.Dashboard;
using EmployeeManagement.Application.DTOs.Master.Employee;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Application.Services.Master;
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
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeService> _logger;
        private readonly IRedisCacheService _redisCacheService; 
        public DashboardService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<EmployeeService> logger
            , IRedisCacheService redisCacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _redisCacheService = redisCacheService;
        } 
        public async Task<DashboardDto> GetDashboardAsync()
        {
            var cacheKey = CacheKeys.DashboardStatistics();

            var cached =
                await _redisCacheService.GetAsync<DashboardDto>(
                    cacheKey);

            if (cached != null)
            {
                return  cached;
            }

            var dashboard = new DashboardDto();
            var employeeQuery = _unitOfWork.Employees.Query();
            dashboard.TotalEmployees = await employeeQuery.CountAsync();

            dashboard.ActiveEmployees =
                await employeeQuery
                    .CountAsync(x => !x.IsDeleted);

            dashboard.InactiveEmployees =
                await employeeQuery
                    .CountAsync(x => x.IsDeleted);

            dashboard.Departments =
                await employeeQuery
                    .Select(x => x.Department)
                    .Distinct()
                    .CountAsync();

            dashboard.NewEmployeesThisMonth =
    await employeeQuery.CountAsync(x => x.JoiningDate.Value.Month == DateTime.UtcNow.Month &&
         x.JoiningDate.Value.Year == DateTime.UtcNow.Year);
            //dashboard.NewEmployeesThisMonth =
            //    await employeeQuery.CountAsync(x =>
            //        (x.JoiningDate == null) ? false: (x.JoiningDate.Value.Month == DateTime.UtcNow.Month) &&
            //        (x.JoiningDate == null) ? false : (x.JoiningDate.Value.Year == DateTime.UtcNow.Year) );

            dashboard.RecentEmployees =
                await employeeQuery
                    .OrderByDescending(x => x.CreatedDate)
                    .Take(5)
                    .Select(x => new RecentEmployeeDto
                    {
                        Id = x.Id,
                        Code = x.Code,
                        Name = x.Name ,
                        DepartmentName = x.Department.Name,
                        JoiningDate = x.JoiningDate
                    })
                    .ToListAsync();

            dashboard.DepartmentStatistics =
                await employeeQuery
                    .GroupBy(x => x.Department)
                    .Select(x => new DepartmentStatisticDto
                    {
                        DepartmentName = x.Key.Name,
                        TotalEmployees = x.Count()
                    })
                    .OrderByDescending(x => x.TotalEmployees)
                    .ToListAsync();

            var currentYear = DateTime.UtcNow.Year;

            var monthlyHiringData = await _unitOfWork.Employees
                .Query()
                .Where(e =>
                    e.JoiningDate.HasValue &&
                    e.JoiningDate.Value.Year == currentYear)
                .GroupBy(e => e.JoiningDate.Value.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    Total = g.Count()
                })
                .OrderBy(x => x.Month)
                .ToListAsync();

            var monthlyHiring = monthlyHiringData
                .Select(x => new MonthlyHiringDto
                {
                    Month = new DateTime(
                        currentYear,
                        x.Month,
                        1).ToString("MMM"),

                    Total = x.Total
                })
                .ToList();
            dashboard.MonthlyHiring = dashboard.MonthlyHiring;
            //dashboard.MonthlyHiring =
            //    await employeeQuery
            //        .Where(x => x.JoiningDate!=null && x.JoiningDate.Value.Year == DateTime.UtcNow.Year)
            //        .GroupBy(x=> x.JoiningDate.Value.Month)
            //        .Select(x => new MonthlyHiringDto
            //        {
            //            Month = new DateTime(
            //                DateTime.UtcNow.Year,
            //                x.Key,
            //                1).ToString("MMM"),
            //            Total = x.Count()
            //        })
            //        .OrderBy(x => x.Month)
            //        .ToListAsync();

            //dashboard.MonthlyHiring =
            //    await employeeQuery
            //        .Where(x => (x.JoiningDate == null)?false:(x.JoiningDate.Value.Year == DateTime.UtcNow.Year))
            //        .GroupBy(x => (x.JoiningDate == null)?0:(x.JoiningDate.Value.Month))
            //        .Select(x => new MonthlyHiringDto
            //        {
            //            Month = new DateTime(
            //                DateTime.UtcNow.Year,
            //                x.Key,
            //                1).ToString("MMM"),
            //            Total = x.Count()
            //        })
            //        .OrderBy(x => x.Month)
            //        .ToListAsync();
            await _redisCacheService.SetAsync(
                   cacheKey, dashboard,
                   TimeSpan.FromMinutes(10));
            return dashboard;
        }
    }
}
