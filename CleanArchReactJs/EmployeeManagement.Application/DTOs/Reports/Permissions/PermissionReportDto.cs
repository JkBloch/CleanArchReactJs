using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.DTOs.Admin.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.DTOs.Reports.Permissions
{
    public class PermissionReportDto : PagedResult<PermissionDto>
    {
        public Guid Id { get; set; }

        public string Code { get; set; } = "";

        public string Name { get; set; } = "";

        public string Email { get; set; } = "";     

        public DateTime CreatedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
