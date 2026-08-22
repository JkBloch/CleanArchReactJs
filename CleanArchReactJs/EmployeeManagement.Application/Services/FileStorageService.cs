using EmployeeManagement.Application.DTOs.FileStorage;
using EmployeeManagement.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Services
{ 
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        public FileStorageService(IWebHostEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _configuration = configuration;
        }

        public async Task<FileUploadResultDto> UploadAsync(
            Stream stream, string fileName, string contentType, string folder,
            CancellationToken cancellationToken = default)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();

            var newFileName = $"{Guid.NewGuid():N}{extension}";

            var uploadFolder = Path.Combine(_environment.WebRootPath, "uploads", folder);

            Directory.CreateDirectory(uploadFolder);

            var physicalPath =
                Path.Combine(uploadFolder, newFileName);

            await using var fileStream =
                new FileStream(physicalPath,FileMode.Create);

            await stream.CopyToAsync(fileStream,cancellationToken);

            var relativePath =
                $"/uploads/{folder}/{newFileName}";

            return new FileUploadResultDto
            {
                FileName = newFileName,
                RelativePath = relativePath,
                Url = relativePath
            };
        }

        public Task DeleteAsync(
            string? filePath,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return Task.CompletedTask;

            var physicalPath =
                Path.Combine(
                    _environment.WebRootPath,
                    filePath.TrimStart('/')
                        .Replace(
                            "/",
                            Path.DirectorySeparatorChar.ToString()));

            if (File.Exists(physicalPath))
                File.Delete(physicalPath);

            return Task.CompletedTask;
        }
    }
}
