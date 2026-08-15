using EmployeeManagement.API.Authorization;
using EmployeeManagement.Application.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static EmployeeManagement.API.Authorization.PermissionData;

namespace EmployeeManagement.API.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwt =
                configuration.GetSection(JwtSettings.SectionName)
                    .Get<JwtSettings>()
                ?? throw new InvalidOperationException("JWT settings are missing.");

            services
                .AddAuthentication(
                    JwtBearerDefaults.AuthenticationScheme)

                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;

                    options.SaveToken = true;

                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,

                            ValidateAudience = true,

                            ValidateLifetime = true,

                            ValidateIssuerSigningKey = true,

                            ValidIssuer = jwt.Issuer,

                            ValidAudience = jwt.Audience,

                            IssuerSigningKey =
                                new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(jwt.SecretKey)),

                            ClockSkew = TimeSpan.Zero
                        };
                });

            return services;
        }

       
            public static IServiceCollection AddPermissionPolicies(
                this IServiceCollection services)
            {
            var lstPage= GetPageList();
            var lstOperation=GetPageOperationList();

                services.AddAuthorization(options =>
                {
                  foreach (var page in lstPage)
                    {
                        foreach(var Operation in lstOperation)
                        {
                            var permission = page + "." + Operation;
                            options.AddPolicy(
                                                  permission,
                                                  policy => policy.RequireClaim(
                                                      page,
                                                      permission));
                        }
                    }
                  

                    //options.AddPolicy(
                    //    Permissions.EmployeeUpdate,
                    //    policy => policy.RequireClaim(
                    //        "Permission",
                    //        Permissions.EmployeeUpdate));

                    //options.AddPolicy(
                    //    Permissions.EmployeeDelete,
                    //    policy => policy.RequireClaim(
                    //        "Permission",
                    //        Permissions.EmployeeDelete));

                    //options.AddPolicy(
                    //    Permissions.EmployeeView,
                    //    policy => policy.RequireClaim(
                    //        "Permission",
                    //        Permissions.EmployeeView));
                });

                return services;
            }
       

    }
}
