using EmployeeManagement.Domain.Common;
using EmployeeManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Entities.Master
{
    public class Employee : AuditableEntity
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public Guid? DepartmentId { get; set; }
        public Department Department { get; set; }
        public Guid? StateId { get; set; }
        public State State { get; set; }
        public Guid? CityId { get; set; }
        public City City { get; set; }
        public decimal? Salary { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? JoiningDate { get; set; }
        public Gender? Gender { get; set; }
        public bool IsActive { get; set; } = true;
        public string? PhotoUrl { get; set; }
        public string? PhotoFileName { get; set; }
    }
}
