using AutoMapper;
using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.Common.SearchExport.Admin;
using EmployeeManagement.Application.Common.SearchExport.Master;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.DTOs.Admin.Permissions;
using EmployeeManagement.Application.DTOs.Admin.RolePermissions;
using EmployeeManagement.Application.DTOs.Admin.Roles;
using EmployeeManagement.Application.DTOs.Admin.UserRoles;
using EmployeeManagement.Application.DTOs.Admin.Users;
using EmployeeManagement.Application.DTOs.Master.City;
using EmployeeManagement.Application.DTOs.Master.Department;
using EmployeeManagement.Application.DTOs.Master.Employee;
using EmployeeManagement.Application.DTOs.Master.State;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Services
{ 
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
            var (permissions, iTotalRecord) =  await PermissionSearchData.GetExportPermissionData(query, request, "pdf"); 

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
            var (roles, iTotalRecord) = await RoleSearchData.GetExportRoleData(query, request, "pdf");


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
            var (rolePermissions, iTotalRecord) = await RolePermissionSearchData.GetExportRolePermissionData(query, request, "pdf");


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
            var (users, iTotalRecord) = await UserSearchData.GetExportUserData(query, request, "pdf");

            try
            {
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
                                            columns.ConstantColumn(80);
                                            columns.ConstantColumn(80);
                                            columns.ConstantColumn(80);
                                            columns.ConstantColumn(80);
                                            columns.ConstantColumn(80);
                                            columns.ConstantColumn(80);
                                            columns.ConstantColumn(80);
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
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<byte[]> ExportUserRolesAsync(SearchUserRoleDto request)
        {
            var query = _unitOfWork.UserRoles.Query()
                .Include(x => x.Role)
                .Include(x => x.User);
            var (userRoles, iTotalRecord) = await UserRoleSearchData.GetExportUserRoleData(query, request, "pdf");


            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);

                    page.Size(PageSizes.A4.Landscape());

                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header()
                        .Text("UserRole List Report")
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

                                    foreach (var userRole in userRoles)
                                    {
                                        table.Cell().Text(userRole.Role.Name);

                                        table.Cell().Text(userRole.User.UserName);

                                        table.Cell().Text(userRole.CreatedDate.ToShortDateString());
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
        public async Task<byte[]> ExportStatesAsync(SearchStateDto request)
        {
            var query = _unitOfWork.States.Query();
            var (states, iTotalRecord) = await StateSearchData.GetExportStateData(query, request, "pdf");


            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);

                    page.Size(PageSizes.A4.Landscape());

                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header()
                        .Text("State List Report")
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

                                    foreach (var state in states)
                                    {
                                        table.Cell().Text(state.Code);

                                        table.Cell().Text(state.Name);

                                        table.Cell().Text(state.CreatedDate.ToShortDateString());
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
        public async Task<byte[]> ExportCitiesAsync(SearchCityDto request)
        {
            var query = _unitOfWork.Cities.Query();
            var (cities, iTotalRecord) = await CitySearchData.GetExportCityData(query, request, "pdf");


            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);

                    page.Size(PageSizes.A4.Landscape());

                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header()
                        .Text("City List Report")
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

                                    foreach (var city in cities)
                                    {
                                        table.Cell().Text(city.Code);

                                        table.Cell().Text(city.Name);

                                        table.Cell().Text(city.CreatedDate.ToShortDateString());
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
        public async Task<byte[]> ExportDepartmentsAsync(SearchDepartmentDto request)
        {
            var query = _unitOfWork.Departments.Query();
            var (departments, iTotalRecord) = await DepartmentSearchData.GetExportDepartmentData(query, request, "pdf");


            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);

                    page.Size(PageSizes.A4.Landscape());

                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header()
                        .Text("Department List Report")
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

                                    foreach (var department in departments)
                                    {
                                        table.Cell().Text(department.Code);

                                        table.Cell().Text(department.Name);

                                        table.Cell().Text(department.CreatedDate.ToShortDateString());
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

        public async Task<byte[]> ExportEmployeesAsync(SearchEmployeeDto request)
        {
            var query = _unitOfWork.Employees.Query();
            var (employees, iTotalRecord) = await EmployeeSearchData.GetExportEmployeeData(query, request, "pdf");


            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);

                    page.Size(PageSizes.A4.Landscape());

                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header()
                        .Text("Employee List Report")
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

                                    foreach (var employee in employees)
                                    {
                                        table.Cell().Text(employee.Code);

                                        table.Cell().Text(employee.Name);

                                        table.Cell().Text(employee.CreatedDate.ToShortDateString());
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
