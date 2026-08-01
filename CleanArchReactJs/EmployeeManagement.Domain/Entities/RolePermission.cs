using EmployeeManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Entities
{
    public class RolePermission : AuditableEntity
    {
        public Guid RoleId { get; set; }

        public Role Role { get; set; } = default!;

        public Guid PermissionId { get; set; }

        public Permission Permission { get; set; } = default!;
    }
}
