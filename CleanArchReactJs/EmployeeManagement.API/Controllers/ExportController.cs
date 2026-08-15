using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.DTOs.Permissions;
using EmployeeManagement.Application.DTOs.RolePermissions;
using EmployeeManagement.Application.DTOs.Roles;
using EmployeeManagement.Application.DTOs.State;
using EmployeeManagement.Application.DTOs.UserRoles;
using EmployeeManagement.Application.DTOs.Users;
using EmployeeManagement.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
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
        #region Permission
        [HttpPost("permissions/excel")]
        public async Task<IActionResult> ExportPermissionsExcel(SearchPermissionDto request)
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
        public async Task<IActionResult> ExportPermissionsPdf(SearchPermissionDto request)
        {
            var pdf = await _pdfExportService.ExportPermissionsAsync(request);

            return File(
                pdf,
                "application/pdf",
                $"PermissionReport_{DateTime.Now:yyyyMMddHHmmss}.pdf");
        }
        #endregion
        #region Role
        [HttpPost("roles/excel")]
        public async Task<IActionResult> ExportRolesExcel(SearchRoleDto request)
        {
            var bytes =
                await _excelExportService
                    .ExportRolesAsync(request);

            return File(
                bytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Roles_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }
        [HttpPost("roles/pdf")]
        public async Task<IActionResult> ExportRolesPdf(SearchRoleDto request)
        {
            var pdf = await _pdfExportService.ExportRolesAsync(request);

            return File(
                pdf,
                "application/pdf",
                $"RoleReport_{DateTime.Now:yyyyMMddHHmmss}.pdf");
        }
        #endregion
        #region RolePermission
        [HttpPost("rolePermissions/excel")]
        public async Task<IActionResult> ExportRolePermissionsExcel(SearchRolePermissionDto request)
        {
            var bytes =
                await _excelExportService
                    .ExportRolePermissionsAsync(request);

            return File(
                bytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"RolePermissions_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }
        [HttpPost("rolePermissions/pdf")]
        public async Task<IActionResult> ExportRolePermissionsPdf(SearchRolePermissionDto request)
        {
            var pdf = await _pdfExportService.ExportRolePermissionsAsync(request);

            return File(
                pdf,
                "application/pdf",
                $"RolePermissionReport_{DateTime.Now:yyyyMMddHHmmss}.pdf");
        }
        #endregion
        #region User
        [HttpPost("users/excel")]
        public async Task<IActionResult> ExportUsersExcel(SearchUserDto request)
        {
            var bytes =
                await _excelExportService
                    .ExportUsersAsync(request);

            return File(
                bytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Users_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }
        [HttpPost("users/pdf")]
        public async Task<IActionResult> ExportUsersPdf(SearchUserDto request)
        {
            var pdf = await _pdfExportService.ExportUsersAsync(request);

            return File(
                pdf,
                "application/pdf",
                $"UserReport_{DateTime.Now:yyyyMMddHHmmss}.pdf");
        }
        #endregion
        #region UserRole
        [HttpPost("userRoles/excel")]
        public async Task<IActionResult> ExportUserRolesExcel(SearchUserRoleDto request)
        {
            var bytes =
                await _excelExportService
                    .ExportUserRolesAsync(request);

            return File(
                bytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"UserRoles_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }
        [HttpPost("userRoles/pdf")]
        public async Task<IActionResult> ExportUserRolesPdf(SearchUserRoleDto request)
        {
            var pdf = await _pdfExportService.ExportUserRolesAsync(request);

            return File(
                pdf,
                "application/pdf",
                $"UserRoleReport_{DateTime.Now:yyyyMMddHHmmss}.pdf");
        }
        #endregion
        #region State
        [HttpPost("states/excel")]
        public async Task<IActionResult> ExportStatesExcel(SearchStateDto request)
        {
            var bytes =
                await _excelExportService
                    .ExportStatesAsync(request);

            return File(
                bytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"States_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }
        [HttpPost("states/pdf")]
        public async Task<IActionResult> ExportStatesPdf(SearchStateDto request)
        {
            var pdf = await _pdfExportService.ExportStatesAsync(request);

            return File(
                pdf,
                "application/pdf",
                $"StateReport_{DateTime.Now:yyyyMMddHHmmss}.pdf");
        }
        #endregion

    }
}
