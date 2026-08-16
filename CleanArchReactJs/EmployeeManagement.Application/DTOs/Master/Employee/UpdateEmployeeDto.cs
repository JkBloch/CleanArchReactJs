using EmployeeManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.DTOs.Master.Employee
{
    public class UpdateEmployeeDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; } = string.Empty;
        public Guid? DepartmentId { get; set; } = null;
        public Guid? StateId { get; set; } = null;
        public Guid? CityId { get; set; } = null;
        public decimal Salary { get; set; } = 0;
        public DateTime? DateOfBirth { get; set; } = null;
        public DateTime? JoiningDate { get; set; } = null;
        public Gender? Gender { get; set; } = null;
        public bool IsActive { get; set; } = true;
    }
}
