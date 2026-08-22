using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.DTOs.FileStorage
{
    public class FileUploadResultDto
    {
        public string FileName { get; set; } = string.Empty;

        public string RelativePath { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;
    }
}
