using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Common
{
    public static class CacheKeys
    {
        public const string EmployeePrefix = "employees:"; 

        public const string DashboardPrefix = "dashboard:";

        public const string ReportPrefix = "reports:";

        public static string Employee(Guid id) => $"{EmployeePrefix}{id}";

        public static string EmployeeSearch(string key) => $"{EmployeePrefix}search:{key}";

        public static string DashboardStatistics() => $"{DashboardPrefix}statistics";
        

    }
}
