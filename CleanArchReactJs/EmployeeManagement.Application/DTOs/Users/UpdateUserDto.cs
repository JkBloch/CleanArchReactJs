using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.DTOs.Users
{
    public class UpdateUserDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public string Email { get; set; } = "";

        public string? PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        public List<Guid> RoleIds { get; set; }
            = new();
    }
}
