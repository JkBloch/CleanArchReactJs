using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.DTOs.RolePermissions
{
    public class SearchRolePermissionDto
    {
        public string? Keyword { get; set; }
        public Guid? RoleId { get; set; }
        public Guid? PermissionId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "Role";
        public bool Descending { get; set; }
    }
}
