using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.DTOs.FileStorage
{
    public class EmployeePhotoDto
    {
        public IFormFile Photo { get; set; } = null!;
    }
}
