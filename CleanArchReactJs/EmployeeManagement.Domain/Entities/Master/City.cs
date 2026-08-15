using EmployeeManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Entities.Master
{
    public class City : AuditableEntity
    {
        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public Guid StateId { get; set; }
        public State State { get; set; }
    }
}
