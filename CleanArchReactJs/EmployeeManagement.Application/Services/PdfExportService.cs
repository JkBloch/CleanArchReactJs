using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Services
{
    using AutoMapper;
    using EmployeeManagement.Application.Common;
    using EmployeeManagement.Application.DTOs;
    using EmployeeManagement.Application.DTOs.Permissions;
    using EmployeeManagement.Application.DTOs.RolePermissions;
    using EmployeeManagement.Application.DTOs.Roles;
    using EmployeeManagement.Application.DTOs.Users;
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
        public async Task<byte[]> ExportPermissionsAsync(SearchPermissionDto request)
        {
            var query = _unitOfWork.Permissions.Query();
            var (permissions, iTotalRecord) =  await SearchExportData.GetExportPermissionData(query, request, "pdf"); 

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

        public async Task<byte[]> ExportRolesAsync(SearchRoleDto request)
        {
            var query = _unitOfWork.Roles.Query();
            var (roles, iTotalRecord) = await SearchExportData.GetExportRoleData(query, request, "pdf");


            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);

                    page.Size(PageSizes.A4.Landscape());

                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header()
                        .Text("Role List Report")
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

                                    foreach (var role in roles)
                                    {
                                        table.Cell().Text(role.Code);

                                        table.Cell().Text(role.Name);

                                        table.Cell().Text(role.CreatedDate.ToShortDateString());
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
        public async Task<byte[]> ExportRolePermissionsAsync(SearchRolePermissionDto request)
        {
            var query = _unitOfWork.RolePermissions.Query()
                .Include(x => x.Role)
                .Include(x => x.Permission);
            var (rolePermissions, iTotalRecord) = await SearchExportData.GetExportRolePermissionData(query, request, "pdf");


            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);

                    page.Size(PageSizes.A4.Landscape());

                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header()
                        .Text("RolePermission List Report")
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
                                        header.Cell().Text("Role").Bold();

                                        header.Cell().Text("Permission").Bold();

                                        header.Cell().Text("CreatedDate").Bold();
                                    });

                                    foreach (var rolePermission in rolePermissions)
                                    {
                                        table.Cell().Text(rolePermission.Role.Name);

                                        table.Cell().Text(rolePermission.Permission.Name);

                                        table.Cell().Text(rolePermission.CreatedDate.ToShortDateString());
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
        public async Task<byte[]> ExportUsersAsync(SearchUserDto request)
        {
            var query = _unitOfWork.Users.Query();
            var (users, iTotalRecord) = await SearchExportData.GetExportUserData(query, request, "pdf");


            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);

                    page.Size(PageSizes.A4.Landscape());

                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header()
                        .Text("User List Report")
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
                                        columns.ConstantColumn(200);
                                        columns.ConstantColumn(200);
                                        columns.ConstantColumn(200);
                                        columns.ConstantColumn(200);
                                        columns.ConstantColumn(200);
                                        columns.ConstantColumn(200);
                                        columns.ConstantColumn(150);
                                    });

                                    table.Header(header =>
                                    {
                                        header.Cell().Text("FirstName").Bold();
                                        header.Cell().Text("LastName").Bold();
                                        header.Cell().Text("UserName").Bold();
                                        header.Cell().Text("Email").Bold();
                                        header.Cell().Text("PhoneNumber").Bold();
                                        header.Cell().Text("IsActive").Bold();
                                        header.Cell().Text("IsLocked").Bold();
                                        header.Cell().Text("AccessFailedCount").Bold();
                                        header.Cell().Text("CreatedDate").Bold();
                                    });

                                    foreach (var user in users)
                                    {
                                        table.Cell().Text(user.FirstName);
                                        table.Cell().Text(user.LastName);
                                        table.Cell().Text(user.UserName);
                                        table.Cell().Text(user.Email);
                                        table.Cell().Text(user.PhoneNumber);
                                        table.Cell().Text(user.IsActive.ToString());
                                        table.Cell().Text(user.IsLocked.ToString());
                                        table.Cell().Text(user.AccessFailedCount.ToString());
                                        table.Cell().Text(user.CreatedDate.ToShortDateString());
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
