using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Services
{
    using AutoMapper;
    using EmployeeManagement.Application.DTOs;
    using EmployeeManagement.Application.Interfaces;
    using EmployeeManagement.Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using QuestPDF.Fluent;
    using QuestPDF.Helpers;
    using QuestPDF.Infrastructure;

    public class PdfExportService : IPdfExportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PdfExportService(IUnitOfWork unitOfWork, IMapper mapper)
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

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);

                    page.Size(PageSizes.A4.Landscape());

                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header()
                        .Text("Permission List Report")
                        .FontSize(22)
                        .Bold();

                    page.Content()
                        .Column(column =>
                        {
                            column.Spacing(15);

                            column.Item()
                                .Text($"Generated : {DateTime.Now:dd-MMM-yyyy HH:mm}");

                            column.Item()
                                .Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.ConstantColumn(120);                                        
                                        columns.ConstantColumn(200);
                                        columns.ConstantColumn(150);
                                    });

                                    table.Header(header =>
                                    {
                                        header.Cell().Text("Code").Bold();

                                        header.Cell().Text("Name").Bold();

                                        header.Cell().Text("CreatedDate").Bold();
                                    });

                                    foreach (var permission in permissions)
                                    {
                                        table.Cell().Text(permission.Code);

                                        table.Cell().Text(permission.Name);

                                        table.Cell().Text(permission.CreatedDate.ToShortDateString());
                                    }
                                });
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");

                            x.CurrentPageNumber();

                            x.Span(" of ");

                            x.TotalPages();
                        });
                });
            }).GeneratePdf();
        }
    }
}
