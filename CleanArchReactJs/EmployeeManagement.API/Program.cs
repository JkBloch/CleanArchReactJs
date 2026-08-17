using Asp.Versioning;
using EmployeeManagement.API.Authorization;
using EmployeeManagement.API.Extensions;
using EmployeeManagement.Application;
using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.Validators;
using EmployeeManagement.Infrastructure;
using EmployeeManagement.Infrastructure.Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuestPDF.Infrastructure;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Text;
 

try
{

    var builder = WebApplication.CreateBuilder(args);
    builder.Services
    .AddApiVersioning(options =>
    {
        options.DefaultApiVersion =
            new ApiVersion(1, 0);

        options.AssumeDefaultVersionWhenUnspecified =
            true;

        options.ReportApiVersions = true;

        options.ApiVersionReader =
            new UrlSegmentApiVersionReader();
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat =
            "'v'VVV";

        options.SubstituteApiVersionInUrl = true;
    });
     
    //added for logs
    var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

    var columnOptions = new ColumnOptions();

    columnOptions.Store.Remove(StandardColumn.Properties);
    columnOptions.Store.Add(StandardColumn.LogEvent);

    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .WriteTo.Console()
        //.WriteTo.MSSqlServer(
        //    connectionString: connectionString,
        //    sinkOptions: new MSSqlServerSinkOptions
        //    {
        //        TableName = "ApplicationLogs",
        //        AutoCreateSqlTable = true
        //    },
        //    columnOptions: columnOptions)
        .CreateLogger();
    builder.Host.UseSerilog();

    //added for pdf report
    QuestPDF.Settings.License = LicenseType.Community;

    //Added all service interface of application
    builder.Services.AddApplication();

    //Added all repository interface of intfrastructure (database)
    builder.Services.AddInfrastructure(builder.Configuration);

    //builder.Services.AddJwtAuthentication(builder.Configuration);

    //Added for allow react 
    builder.Services.AddCors(
        Options =>
        {
            Options.AddPolicy("ReactPolicy", builder =>
            {
                builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
            });
        }
        );
    // Add services to the container.
    builder.Services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection(JwtSettings.SectionName));
    builder.Services.AddJwtAuthentication(builder.Configuration);

    builder.Services.AddPermissionPolicies();
    //builder.Services.AddAuthorization();
    builder.Services.AddEndpointsApiExplorer();
    //builder.Services.AddSwaggerGen();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc(
            "v1",
            new OpenApiInfo
            {
                Title = "Employee Management API",
                Version = "v1",
                Description =
                    "Employee Management System API"
            });

        options.AddSecurityDefinition(
            "Bearer",
            new OpenApiSecurityScheme
            {
                Name = "Authorization",

                Type = SecuritySchemeType.Http,

                Scheme = "bearer",

                BearerFormat = "JWT",

                In = ParameterLocation.Header,

                Description =
                    "Enter JWT token"
            });

        options.AddSecurityRequirement(
            new OpenApiSecurityRequirement
            {
            {
                new OpenApiSecurityScheme
                {
                    Reference =
                        new OpenApiReference
                        {
                            Type =
                                ReferenceType.SecurityScheme,

                            Id = "Bearer"
                        }
                },

                Array.Empty<string>()
            }
            });
    });
    Log.Information("Starting EmployeeManagement API");
    var app = builder.Build();

    // Serilog HTTP request logging
    app.UseSerilogRequestLogging(options =>
    {
        options.MessageTemplate =
            "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";

        options.EnrichDiagnosticContext = (
            diagnosticContext,
            httpContext) =>
        {
            diagnosticContext.Set(
                "UserName",
                httpContext.User.Identity?.Name ?? "Anonymous");

            diagnosticContext.Set(
                "RemoteIP",
                httpContext.Connection.RemoteIpAddress?.ToString());

            diagnosticContext.Set(
                "UserAgent",
                httpContext.Request.Headers.UserAgent.ToString());
        };
    });

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        //app.UseSwaggerUI();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint(
                "/swagger/v1/swagger.json",
                "Employee Management API v1");

            options.RoutePrefix = "swagger";
        });
    }

    app.UseHttpsRedirection();
    //added for allow access react CORS Pollcy
    app.UseCors("ReactPolicy");
    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    //Inserted intial data like user role etc
    await DbInitializer.InitializeAsync(app.Services);
    Log.Information("EmployeeManagement API started successfully");
    app.Run();
}
catch (Exception ex)
{

    Log.Fatal(
         ex,
         "EmployeeManagement API terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}