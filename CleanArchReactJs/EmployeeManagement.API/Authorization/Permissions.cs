using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection;

namespace EmployeeManagement.API.Authorization
{
    public static class PermissionData
    {

        public static class PageList
        {
            public const string Permission = "Permission";
            public const string Role = "Role";
            public const string RolePermission = "RolePermission";
            public const string User = "User";
            public const string UserRole = "UserRole";
            public const string State = "State";
            public const string City = "City";
            public const string Department = "Department";
            public const string Employee = "Employee";
            //public const string Dashboard = "Dashboard";
            //public const string Report = "Report";
            //public const string Audit = "Audit";
            //public const string Settings = "Settings";

        }
        public static class PageOpration
        {
            public const string Create = "Create";
            public const string Delete = "Delete";
            public const string ExportExcel = "ExportExcel";
            public const string ExportPdf = "ExportPdf";
            public const string Restore = "Restore";
            public const string Update = "Update";
            public const string View = "View";
            public const string Manage = "Manage";
        }
        public static List<string> GetPageList()
        {
            // 1. Get all public static fields of the class
            List<string?> lstPage = typeof(PageList)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                // 2. Filter fields that are specifically of type 'string'
                .Where(f => f.FieldType == typeof(string))
                // 3. Extract the value (passing null because it's a static class)
                .Select(f => (string)f.GetValue(null))
                .ToList();
            return lstPage;
        }
        public static List<string> GetPageOperationList()
        {
            List<string?> lstPageOpration = typeof(PageOpration)
                 .GetFields(BindingFlags.Public | BindingFlags.Static)
                 // 2. Filter fields that are specifically of type 'string'
                 .Where(f => f.FieldType == typeof(string))
                 // 3. Extract the value (passing null because it's a static class)
                 .Select(f => (string)f.GetValue(null))
                 .ToList();
            return lstPageOpration;
        }
        public static List<string> GetPermisionList()
        {
            

            // 1. Get all public static fields of the class
            List<string?> lstPage = typeof(PageList)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                // 2. Filter fields that are specifically of type 'string'
                .Where(f => f.FieldType == typeof(string))
                // 3. Extract the value (passing null because it's a static class)
                .Select(f => (string)f.GetValue(null))
                .ToList();

            List<string?> lstPageOpration = typeof(PageOpration)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                // 2. Filter fields that are specifically of type 'string'
                .Where(f => f.FieldType == typeof(string))
                // 3. Extract the value (passing null because it's a static class)
                .Select(f => (string)f.GetValue(null))
                .ToList();

     

            // Combine 1-to-many into a list of strings
            List<string> list = lstPage
                .SelectMany(page => lstPageOpration, (cat, perms) => $"{cat}.:{perms}")
                .ToList();
            return list;
        }
        //public static class Permissions
        //{
        //    public const string EmployeeView = "Employee.View";
        //    public const string EmployeeCreate = "Employee.Create";
        //    public const string EmployeeUpdate = "Employee.Update";
        //    public const string EmployeeDelete = "Employee.Delete";
        //    public const string EmployeeRestore = "Employee.Restore";

        //    public const string ReportExportExcel = "Report.ExportExcel";
        //    public const string ReportExportPdf = "Report.ExportPdf";

        //    public const string DashboardView = "Dashboard.View";

        //    public const string AuditView = "Audit.View";

        //    public const string SettingsManage = "Settings.Manage";
        //}
    }

}