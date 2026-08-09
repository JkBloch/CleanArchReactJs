using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.DTOs.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.DTOs.Users
{
    public class PagedUserResponseDto : PagedResult<UserDto>
    {
    }
}
