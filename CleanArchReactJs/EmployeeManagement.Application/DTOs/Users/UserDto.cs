using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.DTOs.Users
{ 
    public class UserDto
    {
        public Guid Id { get; set; }

        public string FullName { get; set; } = "";

        public string UserName { get; set; } = "";

        public string Email { get; set; } = "";

        public string? PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        public bool IsLocked { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public IEnumerable<string> Roles { get; set; }
            = Enumerable.Empty<string>();
    }
}
