using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.DTOs.Log
{
    public class ApplicationLogDto
    {
        public Guid Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Level { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public string? Exception { get; set; }
    }
}
