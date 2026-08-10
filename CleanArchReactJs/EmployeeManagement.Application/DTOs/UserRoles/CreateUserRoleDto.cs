using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.DTOs.UserRoles
{
    public class CreateUserRoleDto
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
    }
}
