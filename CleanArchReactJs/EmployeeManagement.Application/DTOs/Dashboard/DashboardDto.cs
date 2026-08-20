using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.DTOs.Dashboard
{
    public class DashboardDto
    {
        public int TotalEmployees { get; set; }

        public int ActiveEmployees { get; set; }

        public int InactiveEmployees { get; set; }

        public int Departments { get; set; }

        public int NewEmployeesThisMonth { get; set; }

        public List<RecentEmployeeDto> RecentEmployees { get; set; }
            = new();

        public List<DepartmentStatisticDto> DepartmentStatistics { get; set; }
            = new();

        public List<MonthlyHiringDto> MonthlyHiring { get; set; }
            = new();
    }
}
