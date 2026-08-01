using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.DTOs.Users
{
    public class UserSearchDto
    {
        public string? Keyword { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsLocked { get; set; }

        public Guid? RoleId { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string SortBy { get; set; } = "FirstName";

        public bool Descending { get; set; }
    }
}
