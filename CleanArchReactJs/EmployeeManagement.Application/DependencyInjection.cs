using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Application.Mapping;
using EmployeeManagement.Application.Services;
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
            //services.AddScoped<IEmployeeService, EmployeeService>();

            //services.AddScoped<IAuthService, AuthService>();

            //services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
