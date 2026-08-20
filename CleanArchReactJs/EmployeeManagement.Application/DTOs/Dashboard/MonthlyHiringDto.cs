using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.DTOs.Dashboard
{
    public class MonthlyHiringDto
    {
        public string Month { get; set; } = string.Empty;

        public int Total { get; set; }
    }
}
