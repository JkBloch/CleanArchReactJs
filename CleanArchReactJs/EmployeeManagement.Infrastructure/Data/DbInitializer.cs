using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Entities.Admin;
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
                    Code = "P0001",
                    Name = "ALL"
                };
                await context.Permissions.AddAsync(permission);
                await context.SaveChangesAsync();
            }
            if (!context.Roles.Any())
            {
                var role = new Role
                {
                    Code = "R0001",
                    Name = "Admin"
                };
                await context.Roles.AddAsync(role);
                await context.SaveChangesAsync();
            }
            if (!context.RolePermissions.Any())
            {
                var role=  context.Roles.Where(x => x.Code == "R0001").FirstOrDefault();
                var permission = context.Permissions.Where(x => x.Code == "P0001").FirstOrDefault();

                if (role != null && permission != null)
                {
                    var rolePermission = new RolePermission
                    {
                        RoleId = role.Id,
                        PermissionId = permission.Id
                    };
                    await context.RolePermissions.AddAsync(rolePermission);
                    await context.SaveChangesAsync();

                }
            }
            if (!context.Users.Any())
            {
                var user = new User
                {                 
                    FirstName = "Javed",
                    LastName = "Bloch",
                    UserName = "jkbloch",
                    Email = "blochjavedk@gmail.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin@123"),
                    IsActive = true,
                    IsLocked = false,
                    AccessFailedCount = 0
                };
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }
            if (!context.UserRoles.Any())
            {
                var role = context.Roles.Where(x => x.Code == "R0001").FirstOrDefault();
                var user = context.Users.Where(x => x.UserName == "jkbloch").FirstOrDefault();

                if (role != null && user != null)
                {
                    var userRole = new UserRole
                    {
                        RoleId = role.Id,
                        UserId = user.Id
                    };
                    await context.UserRoles.AddAsync(userRole);
                    await context.SaveChangesAsync();

                }
            }
        }
    }
}
