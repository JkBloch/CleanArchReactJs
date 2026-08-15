using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.DTOs.State
{
    public class SearchStateDto
    {
        public string? Keyword { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "Name";
        public bool Descending { get; set; }
    }
}
