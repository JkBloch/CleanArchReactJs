using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.DTOs.Dashboard
{
    public class RecentEmployeeDto
    {
        public Guid Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string DepartmentName { get; set; } = string.Empty;

        public DateTime? JoiningDate { get; set; }
    }
}
