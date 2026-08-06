using EmployeeManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Interfaces
{
    public interface IExcelExportService
    {
        Task<byte[]> ExportPermissionsAsync(ExportRequestDto request);
    }
}
