using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.DTOs.Dashboard
{
    public class DepartmentStatisticDto
    {
        public string DepartmentName { get; set; } = string.Empty;

        public int TotalEmployees { get; set; }
    }
}
