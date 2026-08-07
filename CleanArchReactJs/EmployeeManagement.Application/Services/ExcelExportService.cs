using AutoMapper;
using ClosedXML.Excel;
using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.DTOs.Permissions;
using EmployeeManagement.Application.DTOs.Roles;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace EmployeeManagement.Application.Services
{
    public class ExcelExportService : IExcelExportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExcelExportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #region Permission
        #endregion
        public async Task<byte[]> ExportPermissionsAsync(SearchPermissionDto request)
        {
            var query = _unitOfWork.Permissions.Query();
             
            var (permissions, iTotalRecord) = await SearchExportData.GetExportPermissionData(query, request, "excel");

            using var workbook = new XLWorkbook();

            CreateSummarySheet(
                workbook,
                permissions);

            CreatePermissionSheet(
                workbook, 
                permissions);

            using var stream =
                new MemoryStream();

            workbook.SaveAs(stream);

            return stream.ToArray();
        }
        private static void CreateSummarySheet(XLWorkbook workbook,List<Permission> permissions)
        {
            var ws =
                workbook.Worksheets.Add("Summary");

            ws.Cell("A1").Value =
                "Permission Report";

            ws.Cell("A1").Style
                .Font.Bold = true;

            ws.Cell("A1").Style
                .Font.FontSize = 20;

            ws.Cell("A3").Value =
                "Generated";

            ws.Cell("B3").Value =
                DateTime.Now;

            ws.Cell("A5").Value =
                "Total Permissions";

            ws.Cell("B5").Value =
                permissions.Count;

            ws.Cell("A6").Value =
                "Names";

            ws.Cell("B6").Value =
                permissions
                .Select(x => x.Name)
                .Distinct()
                .Count();

            ws.Columns().AdjustToContents();
        }
        private static void CreatePermissionSheet(XLWorkbook workbook,List<Permission> permissions)
        {
            var ws =
                workbook.Worksheets.Add("Permissions");

            ws.Cell(1, 1).Value = "Code";
            ws.Cell(1, 2).Value = "Name";
            ws.Cell(1, 3).Value = "Created";

            var header =
                ws.Range("A1:C1");

            header.Style.Font.Bold = true;

            header.Style.Fill.BackgroundColor =
                XLColor.SteelBlue;

            header.Style.Font.FontColor =
                XLColor.White;

            int row = 2;

            foreach (var permission in permissions)
            {
                ws.Cell(row, 1).Value =
                    permission.Code;

                ws.Cell(row, 2).Value =
                    permission.Name;
                ws.Cell(row, 3).Value =
                    permission.CreatedDate;

                row++;
            }

            ws.Columns()
                .AdjustToContents();

            ws.SheetView
                .FreezeRows(1);

            ws.RangeUsed()
                .SetAutoFilter();
        }

        #region Role
        public async Task<byte[]> ExportRolesAsync(SearchRoleDto request)
        {
            var query = _unitOfWork.Roles.Query();

            var (roles, iTotalRecord) = await SearchExportData.GetExportRoleData(query, request, "excel");


            using var workbook =
                new XLWorkbook();

            CreateSummarySheet(
                workbook,
                roles);

            CreateRoleSheet(
                workbook,
                roles);

            using var stream =
                new MemoryStream();

            workbook.SaveAs(stream);

            return stream.ToArray();
        }
        private static void CreateSummarySheet(XLWorkbook workbook, List<Role> roles)
        {
            var ws =
                workbook.Worksheets.Add("Summary");

            ws.Cell("A1").Value =
                "Role Report";

            ws.Cell("A1").Style
                .Font.Bold = true;

            ws.Cell("A1").Style
                .Font.FontSize = 20;

            ws.Cell("A3").Value =
                "Generated";

            ws.Cell("B3").Value =
                DateTime.Now;

            ws.Cell("A5").Value =
                "Total Roles";

            ws.Cell("B5").Value =
                roles.Count;

            ws.Cell("A6").Value =
                "Names";

            ws.Cell("B6").Value =
                roles
                .Select(x => x.Name)
                .Distinct()
                .Count();

            ws.Columns().AdjustToContents();
        }
        private static void CreateRoleSheet(XLWorkbook workbook, List<Role> roles)
        {
            var ws =
                workbook.Worksheets.Add("Roles");

            ws.Cell(1, 1).Value = "Code";
            ws.Cell(1, 2).Value = "Name";
            ws.Cell(1, 3).Value = "Created";

            var header =
                ws.Range("A1:C1");

            header.Style.Font.Bold = true;

            header.Style.Fill.BackgroundColor =
                XLColor.SteelBlue;

            header.Style.Font.FontColor =
                XLColor.White;

            int row = 2;

            foreach (var role in roles)
            {
                ws.Cell(row, 1).Value =
                    role.Code;

                ws.Cell(row, 2).Value =
                    role.Name;
                ws.Cell(row, 3).Value =
                    role.CreatedDate;

                row++;
            }

            ws.Columns()
                .AdjustToContents();

            ws.SheetView
                .FreezeRows(1);

            ws.RangeUsed()
                .SetAutoFilter();
        }

        #endregion
    }
}