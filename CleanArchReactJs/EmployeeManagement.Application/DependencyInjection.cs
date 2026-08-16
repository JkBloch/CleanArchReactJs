using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Application.Interfaces.Admin;
using EmployeeManagement.Application.Interfaces.Master;
using EmployeeManagement.Application.Mapping.Admin;
using EmployeeManagement.Application.Services;
using EmployeeManagement.Application.Services.Admin;
using EmployeeManagement.Application.Services.Master;
using EmployeeManagement.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(PermissionProfile));
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IExcelExportService, ExcelExportService>();
            services.AddScoped<IPdfExportService, PdfExportService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRolePermissionService, RolePermissionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IDepartmentService, DepartmentService>(); 
            services.AddScoped<IEmployeeService, EmployeeService>();


            return services;
        }
    }
}
