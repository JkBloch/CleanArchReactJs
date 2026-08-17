using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.DTOs.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Interfaces.Admin
{
    public interface IApplicationLogService
    {
        Task<List<ApplicationLogDto>> GetAllAsync();

        Task<List<ApplicationLogDto>> SearchAsync(string? search);
    }
}
