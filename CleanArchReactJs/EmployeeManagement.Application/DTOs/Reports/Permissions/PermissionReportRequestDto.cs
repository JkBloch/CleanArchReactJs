using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.DTOs.Reports.Permissions
{
    public class PermissionReportRequestDto
    {
        public string? Keyword { get; set; }
        public string? Code { get; set; }

        public string? Name { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string SortBy { get; set; } = "Name";

        public bool Descending { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}
