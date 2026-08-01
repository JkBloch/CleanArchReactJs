using EmployeeManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Entities
{
    public class UserRole : AuditableEntity
    {
        public Guid UserId { get; set; }

        public User User { get; set; } = default!;

        public Guid RoleId { get; set; }

        public Role Role { get; set; } = default!;
    }
}
