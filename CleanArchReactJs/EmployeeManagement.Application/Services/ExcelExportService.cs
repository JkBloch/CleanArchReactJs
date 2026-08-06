using AutoMapper;
using ClosedXML.Excel;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace EmployeeManagement.Application.Services
{
    public class ExcelExportService
    : IExcelExportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExcelExportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<byte[]> ExportPermissionsAsync(ExportRequestDto request)
        {
            var query = _unitOfWork.Permissions.Query();

            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                query = query.Where(x =>
                    x.Code.Contains(request.Keyword) ||
                    x.Name.Contains(request.Keyword));
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(x =>
                    x.Name == request.Name);
            }

            var permissions =
                await query
                .OrderBy(x => x.Code)
                .ToListAsync();

            using var workbook =
                new XLWorkbook();

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

    }
}