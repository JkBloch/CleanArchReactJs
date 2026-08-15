using AutoMapper;
using ClosedXML.Excel;
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
using EmployeeManagement.Application.DTOs.Master.State;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities.Admin;
using EmployeeManagement.Domain.Entities.Master;
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
        public async Task<byte[]> ExportPermissionsAsync(SearchPermissionDto request)
        {
            var query = _unitOfWork.Permissions.Query();
             
            var (permissions, iTotalRecord) = await PermissionSearchData.GetExportPermissionData(query, request, "excel");

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
        #endregion
        #region Role
        public async Task<byte[]> ExportRolesAsync(SearchRoleDto request)
        {
            var query = _unitOfWork.Roles.Query();

            var (roles, iTotalRecord) = await RoleSearchData.GetExportRoleData(query, request, "excel");


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

        #region RolePermission
        public async Task<byte[]> ExportRolePermissionsAsync(SearchRolePermissionDto request)
        {
            var query = _unitOfWork.RolePermissions.Query()
                .Include(x=>x.Role)
                .Include(x=>x.Permission);

            var (rolePermissions, iTotalRecord) = await RolePermissionSearchData.GetExportRolePermissionData(query, request, "excel");


            using var workbook =
                new XLWorkbook();

            CreateSummarySheet(
                workbook,
                rolePermissions);

            CreateRolePermissionSheet(
                workbook,
                rolePermissions);

            using var stream =
                new MemoryStream();

            workbook.SaveAs(stream);

            return stream.ToArray();
        }
        private static void CreateSummarySheet(XLWorkbook workbook, List<RolePermission> rolePermissions)
        {
            var ws =
                workbook.Worksheets.Add("Summary");

            ws.Cell("A1").Value =
                "RolePermission Report";

            ws.Cell("A1").Style
                .Font.Bold = true;

            ws.Cell("A1").Style
                .Font.FontSize = 20;

            ws.Cell("A3").Value =
                "Generated";

            ws.Cell("B3").Value =
                DateTime.Now;

            ws.Cell("A5").Value =
                "Total RolePermissions";

            ws.Cell("B5").Value =
                rolePermissions.Count;

            ws.Cell("A6").Value =
                "Roles";

            ws.Cell("B6").Value =
                rolePermissions
                .Select(x => x.Role)
                .Distinct()
                .Count();

            ws.Columns().AdjustToContents();
        }
        private static void CreateRolePermissionSheet(XLWorkbook workbook, List<RolePermission> rolePermissions)
        {
            var ws =
                workbook.Worksheets.Add("RolePermissions");

            ws.Cell(1, 1).Value = "Role";
            ws.Cell(1, 2).Value = "Permission";
            ws.Cell(1, 3).Value = "Created";

            var header =
                ws.Range("A1:C1");

            header.Style.Font.Bold = true;

            header.Style.Fill.BackgroundColor =
                XLColor.SteelBlue;

            header.Style.Font.FontColor =
                XLColor.White;

            int row = 2;

            foreach (var rolePermission in rolePermissions)
            {
                ws.Cell(row, 1).Value =
                    rolePermission.Role.Name;

                ws.Cell(row, 2).Value =
                    rolePermission.Permission.Name;
                ws.Cell(row, 3).Value =
                    rolePermission.CreatedDate;

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

        #region User
        public async Task<byte[]> ExportUsersAsync(SearchUserDto request)
        {
            var query = _unitOfWork.Users.Query();

            var (users, iTotalRecord) = await UserSearchData.GetExportUserData(query, request, "excel");


            using var workbook =
                new XLWorkbook();

            CreateSummarySheet(
                workbook,
                users);

            CreateUserSheet(
                workbook,
                users);

            using var stream =
                new MemoryStream();

            workbook.SaveAs(stream);

            return stream.ToArray();
        }
        private static void CreateSummarySheet(XLWorkbook workbook, List<User> users)
        {
            var ws =
                workbook.Worksheets.Add("Summary");

            ws.Cell("A1").Value =
                "User Report";

            ws.Cell("A1").Style
                .Font.Bold = true;

            ws.Cell("A1").Style
                .Font.FontSize = 20;

            ws.Cell("A3").Value =
                "Generated";

            ws.Cell("B3").Value =
                DateTime.Now;

            ws.Cell("A5").Value =
                "Total Users";

            ws.Cell("B5").Value =
                users.Count;

            ws.Cell("A6").Value =
                "Names";

            ws.Cell("B6").Value =
                users
                .Select(x => x.UserName)
                .Distinct()
                .Count();

            ws.Columns().AdjustToContents();
        }
        private static void CreateUserSheet(XLWorkbook workbook, List<User> users)
        {
            var ws =
                workbook.Worksheets.Add("Users");

            ws.Cell(1, 1).Value = "FirstName";
            ws.Cell(1, 2).Value = "LastName";
            ws.Cell(1, 3).Value = "UserName";
            ws.Cell(1, 4).Value = "Email";
            ws.Cell(1, 5).Value = "PhoneNumber";
            ws.Cell(1, 6).Value = "IsActive";
            ws.Cell(1, 7).Value = "IsLocked";
            ws.Cell(1, 8).Value = "AccessFailedCount";


            var header =
                ws.Range("A1:H1");

            header.Style.Font.Bold = true;

            header.Style.Fill.BackgroundColor =
                XLColor.SteelBlue;

            header.Style.Font.FontColor =
                XLColor.White;

            int row = 2;

            foreach (var user in users)
            {
                ws.Cell(row, 1).Value = user.FirstName;
                ws.Cell(row, 2).Value = user.LastName;
                ws.Cell(row, 3).Value = user.UserName;
                ws.Cell(row, 4).Value = user.Email;
                ws.Cell(row, 5).Value = user.PhoneNumber;
                ws.Cell(row, 6).Value = user.IsActive;
                ws.Cell(row, 7).Value = user.IsLocked;
                ws.Cell(row, 8).Value = user.AccessFailedCount;
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

        #region UserRole
        public async Task<byte[]> ExportUserRolesAsync(SearchUserRoleDto request)
        {
            var query = _unitOfWork.UserRoles.Query()
                .Include(x => x.Role)
                .Include(x => x.User);

            var (userRoles, iTotalRecord) = await UserRoleSearchData.GetExportUserRoleData(query, request, "excel");


            using var workbook =
                new XLWorkbook();

            CreateSummarySheet(
                workbook,
                userRoles);

            CreateUserRoleSheet(
                workbook,
                userRoles);

            using var stream =
                new MemoryStream();

            workbook.SaveAs(stream);

            return stream.ToArray();
        }
        private static void CreateSummarySheet(XLWorkbook workbook, List<UserRole> userRoles)
        {
            var ws =
                workbook.Worksheets.Add("Summary");

            ws.Cell("A1").Value =
                "UserRole Report";

            ws.Cell("A1").Style
                .Font.Bold = true;

            ws.Cell("A1").Style
                .Font.FontSize = 20;

            ws.Cell("A3").Value =
                "Generated";

            ws.Cell("B3").Value =
                DateTime.Now;

            ws.Cell("A5").Value =
                "Total UserRoles";

            ws.Cell("B5").Value =
                userRoles.Count;

            ws.Cell("A6").Value =
                "Roles";

            ws.Cell("B6").Value =
                userRoles
                .Select(x => x.Role)
                .Distinct()
                .Count();

            ws.Columns().AdjustToContents();
        }
        private static void CreateUserRoleSheet(XLWorkbook workbook, List<UserRole> userRoles)
        {
            var ws =
                workbook.Worksheets.Add("UserRoles");

            ws.Cell(1, 1).Value = "Role";
            ws.Cell(1, 2).Value = "User";
            ws.Cell(1, 3).Value = "Created";

            var header =
                ws.Range("A1:C1");

            header.Style.Font.Bold = true;

            header.Style.Fill.BackgroundColor =
                XLColor.SteelBlue;

            header.Style.Font.FontColor =
                XLColor.White;

            int row = 2;

            foreach (var userRole in userRoles)
            {
                ws.Cell(row, 1).Value =
                    userRole.Role.Name;

                ws.Cell(row, 2).Value =
                    userRole.User.UserName;
                ws.Cell(row, 3).Value =
                    userRole.CreatedDate;

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

        #region State
        public async Task<byte[]> ExportStatesAsync(SearchStateDto request)
        {
            var query = _unitOfWork.States.Query();

            var (states, iTotalRecord) = await StateSearchData.GetExportStateData(query, request, "excel");


            using var workbook =
                new XLWorkbook();

            CreateSummarySheet(
                workbook,
                states);

            CreateStateSheet(
                workbook,
                states);

            using var stream =
                new MemoryStream();

            workbook.SaveAs(stream);

            return stream.ToArray();
        }
        private static void CreateSummarySheet(XLWorkbook workbook, List<State> states)
        {
            var ws =
                workbook.Worksheets.Add("Summary");

            ws.Cell("A1").Value =
                "State Report";

            ws.Cell("A1").Style
                .Font.Bold = true;

            ws.Cell("A1").Style
                .Font.FontSize = 20;

            ws.Cell("A3").Value =
                "Generated";

            ws.Cell("B3").Value =
                DateTime.Now;

            ws.Cell("A5").Value =
                "Total States";

            ws.Cell("B5").Value =
                states.Count;

            ws.Cell("A6").Value =
                "Names";

            ws.Cell("B6").Value =
                states
                .Select(x => x.Name)
                .Distinct()
                .Count();

            ws.Columns().AdjustToContents();
        }
        private static void CreateStateSheet(XLWorkbook workbook, List<State> states)
        {
            var ws =
                workbook.Worksheets.Add("States");

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

            foreach (var state in states)
            {
                ws.Cell(row, 1).Value =
                    state.Code;

                ws.Cell(row, 2).Value =
                    state.Name;
                ws.Cell(row, 3).Value =
                    state.CreatedDate;

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

        #region City
        public async Task<byte[]> ExportCitiesAsync(SearchCityDto request)
        {
            var query = _unitOfWork.Cities.Query();

            var (cities, iTotalRecord) = await CitySearchData.GetExportCityData(query, request, "excel");


            using var workbook =
                new XLWorkbook();

            CreateSummarySheet(
                workbook,
                cities);

            CreateCitySheet(
                workbook,
                cities);

            using var stream =
                new MemoryStream();

            workbook.SaveAs(stream);

            return stream.ToArray();
        }
        private static void CreateSummarySheet(XLWorkbook workbook, List<City> cities)
        {
            var ws =
                workbook.Worksheets.Add("Summary");

            ws.Cell("A1").Value =
                "City Report";

            ws.Cell("A1").Style
                .Font.Bold = true;

            ws.Cell("A1").Style
                .Font.FontSize = 20;

            ws.Cell("A3").Value =
                "Generated";

            ws.Cell("B3").Value =
                DateTime.Now;

            ws.Cell("A5").Value =
                "Total Cities";

            ws.Cell("B5").Value =
                cities.Count;

            ws.Cell("A6").Value =
                "Names";

            ws.Cell("B6").Value =
                cities
                .Select(x => x.Name)
                .Distinct()
                .Count();

            ws.Columns().AdjustToContents();
        }
        private static void CreateCitySheet(XLWorkbook workbook, List<City> cities)
        {
            var ws =
                workbook.Worksheets.Add("Cities");

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

            foreach (var city in cities)
            {
                ws.Cell(row, 1).Value =
                    city.Code;

                ws.Cell(row, 2).Value =
                    city.Name;
                ws.Cell(row, 3).Value =
                    city.CreatedDate;

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

        #region Department
        public async Task<byte[]> ExportDepartmentsAsync(SearchDepartmentDto request)
        {
            var query = _unitOfWork.Departments.Query();

            var (departments, iTotalRecord) = await DepartmentSearchData.GetExportDepartmentData(query, request, "excel");


            using var workbook =
                new XLWorkbook();

            CreateSummarySheet(
                workbook,
                departments);

            CreateDepartmentSheet(
                workbook,
                departments);

            using var stream =
                new MemoryStream();

            workbook.SaveAs(stream);

            return stream.ToArray();
        }
        private static void CreateSummarySheet(XLWorkbook workbook, List<Department> departments)
        {
            var ws =
                workbook.Worksheets.Add("Summary");

            ws.Cell("A1").Value =
                "Department Report";

            ws.Cell("A1").Style
                .Font.Bold = true;

            ws.Cell("A1").Style
                .Font.FontSize = 20;

            ws.Cell("A3").Value =
                "Generated";

            ws.Cell("B3").Value =
                DateTime.Now;

            ws.Cell("A5").Value =
                "Total Departments";

            ws.Cell("B5").Value =
                departments.Count;

            ws.Cell("A6").Value =
                "Names";

            ws.Cell("B6").Value =
                departments
                .Select(x => x.Name)
                .Distinct()
                .Count();

            ws.Columns().AdjustToContents();
        }
        private static void CreateDepartmentSheet(XLWorkbook workbook, List<Department> departments)
        {
            var ws =
                workbook.Worksheets.Add("Departments");

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

            foreach (var department in departments)
            {
                ws.Cell(row, 1).Value =
                    department.Code;

                ws.Cell(row, 2).Value =
                    department.Name;
                ws.Cell(row, 3).Value =
                    department.CreatedDate;

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