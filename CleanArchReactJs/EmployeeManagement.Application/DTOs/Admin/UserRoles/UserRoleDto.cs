using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.DTOs.Admin.UserRoles
{
    public class UserRoleDto
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }

    }
}
