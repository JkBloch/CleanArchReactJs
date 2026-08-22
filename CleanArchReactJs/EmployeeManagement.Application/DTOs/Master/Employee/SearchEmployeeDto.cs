using EmployeeManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.DTOs.Master.Employee
{
    public class SearchEmployeeDto
    {
        public string? Keyword { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Email { get; set; }
        public Guid? DepartmentId { get; set; } 
        public Guid? StateId { get; set; } 
        public Guid? CityId { get; set; } 
        public decimal? SalaryFrom { get; set; }
        public decimal? SalaryTo { get; set; }
        public DateTime? DateOfBirthFrom { get; set; }
        public DateTime? DateOfBirthTo { get; set; }
        public DateTime? JoiningDateFrom { get; set; }
        public DateTime? JoiningDateTo { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "Name";
        public bool Descending { get; set; }
    }
}
