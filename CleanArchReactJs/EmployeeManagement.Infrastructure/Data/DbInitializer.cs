using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider
                .GetRequiredService<AppDbContext>();

            var logger = scope.ServiceProvider
                .GetRequiredService<ILogger<AppDbContext>>();

            try
            {
                // Apply pending migrations
                await context.Database.MigrateAsync();

                // await SeedDepartmentsAsync(context);

                await SeedAsync(context);

                logger.LogInformation("Database initialized successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Database initialization failed.");
                throw;
            }
        }

        public static async Task SeedAsync(AppDbContext context)
        {
            if (!context.Permissions.Any())
            {
                var permission = new Permission
                {
                    Code = "ALL",
                    Name = "ALL"
                };
                await context.Permissions.AddAsync(permission);
                //var admin = new User
                //{
                //    FirstName = "System",
                //    LastName = "Administrator",
                //    UserName = "admin",
                //    Email = "admin@company.com",
                //    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                //    Role = UserRole.Admin,
                //    IsActive = true
                //};

                //await context.Users.AddAsync(admin);

                await context.SaveChangesAsync();
            }
            if (!context.Roles.Any())
            {
                var role = new Role
                {
                    Code = "R001",
                    Name = "Admin"
                };
                await context.Roles.AddAsync(role);
                await context.SaveChangesAsync();
            }


        }
    }
}
