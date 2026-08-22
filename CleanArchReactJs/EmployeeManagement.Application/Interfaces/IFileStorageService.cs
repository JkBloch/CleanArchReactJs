using EmployeeManagement.Application.DTOs.FileStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Interfaces
{
    public interface IFileStorageService
    {
        Task<FileUploadResultDto> UploadAsync(
            Stream stream,
            string fileName,
            string contentType,
            string folder,
            CancellationToken cancellationToken = default);

        Task DeleteAsync(
            string? filePath,
            CancellationToken cancellationToken = default);
    }
}
