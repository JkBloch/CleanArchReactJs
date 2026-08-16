using EmployeeManagement.Domain.Common;
using EmployeeManagement.Domain.Entities.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Entities.Master
{
    public class State : AuditableEntity
    {
        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public ICollection<City> Cities { get; set; } = new List<City>();
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();

    }
}
