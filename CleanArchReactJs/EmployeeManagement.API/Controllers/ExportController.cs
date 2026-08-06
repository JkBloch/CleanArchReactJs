using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/export")]
    public class ExportController : ControllerBase
    {
        private readonly IExcelExportService _excelExportService;
        private readonly IPdfExportService _pdfExportService;

        public ExportController(IExcelExportService excelExportService, IPdfExportService pdfExportService)
        {
            _excelExportService = excelExportService;
            _pdfExportService = pdfExportService;
        }

        [HttpPost("permissions/excel")]
        public async Task<IActionResult> ExportPermissions(
            ExportRequestDto request)
        {
            var bytes =
                await _excelExportService
                    .ExportPermissionsAsync(request);

            return File(
                bytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Permissions_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }
        [HttpPost("permissions/pdf")]
        public async Task<IActionResult> ExportPdf(ExportRequestDto request)
        {
            var pdf = await _pdfExportService.ExportPermissionsAsync(request);

            return File(
                pdf,
                "application/pdf",
                $"PermissionReport_{DateTime.Now:yyyyMMddHHmmss}.pdf");
        }
    }
}
