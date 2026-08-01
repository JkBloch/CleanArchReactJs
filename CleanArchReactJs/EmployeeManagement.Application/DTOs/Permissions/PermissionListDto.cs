using EmployeeManagement.Application.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.DTOs.Permissions
{
    internal class PermissionListDto
    {
        public IEnumerable<PermissionDto> Items { get; set; } = Enumerable.Empty<PermissionDto>();
        public int TotalRecords { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
