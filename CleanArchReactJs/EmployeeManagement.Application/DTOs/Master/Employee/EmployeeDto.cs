using EmployeeManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.DTOs.Master.Employee
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public Guid? DepartmentId { get; set; } = Guid.Empty;
        public Guid? StateId { get; set; } = Guid.Empty;
        public Guid? CityId { get; set; } = Guid.Empty;
        public decimal? Salary { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? JoiningDate { get; set; }
        public Gender Gender { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string StateName { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
        public string? PhotoUrl { get; set; }

    }
}
