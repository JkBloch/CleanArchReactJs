using AutoMapper;
using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.DTOs.Log;
using EmployeeManagement.Application.Interfaces.Admin;
using EmployeeManagement.Domain.Entities;
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
    public class ApplicationLogService : IApplicationLogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ApplicationLogService> _logger;
        public ApplicationLogService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ApplicationLogService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<List<ApplicationLogDto>> GetAllAsync()
        {
            return await _unitOfWork.ApplicationLogs.Query()
                .OrderByDescending(x => x.TimeStamp)
                .Take(500)
                .Select(x => new ApplicationLogDto
                {
                    Id = x.Id,
                    TimeStamp = x.TimeStamp,
                    Level = x.Level,
                    Message = x.Message,
                    Exception = x.Exception
                })
                .ToListAsync();
        }

        public async Task<List<ApplicationLogDto>> SearchAsync(
            string? search)
        {
            var query = _unitOfWork.ApplicationLogs.Query();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x =>
                    x.Message.Contains(search) ||
                    x.Level.Contains(search) ||
                    x.Exception != null &&
                     x.Exception.Contains(search));
            }

            return await query
                .OrderByDescending(x => x.TimeStamp)
                .Take(500)
                .Select(x => new ApplicationLogDto
                {
                    Id = x.Id,
                    TimeStamp = x.TimeStamp,
                    Level = x.Level,
                    Message = x.Message,
                    Exception = x.Exception
                })
                .ToListAsync();
        }
    }

}
