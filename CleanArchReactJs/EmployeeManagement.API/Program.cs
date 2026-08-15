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
using QuestPDF.Infrastructure;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


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
        Options.AddPolicy("ReactPolicy", builder => {
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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JwtSettings>(
builder.Configuration.GetSection(JwtSettings.SectionName));
builder.Services.AddJwtAuthentication(builder.Configuration);
//var jwt = builder.Configuration
//    .GetSection(JwtSettings.SectionName)
//    .Get<JwtSettings>()!;

//builder.Services
//    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters =
//            new TokenValidationParameters
//            {
//                ValidateIssuer = true,

//                ValidateAudience = true,

//                ValidateLifetime = true,

//                ValidateIssuerSigningKey = true,

//                ValidIssuer = jwt.Issuer,

//                ValidAudience = jwt.Audience,

//                IssuerSigningKey =
//                    new SymmetricSecurityKey(
//                        Encoding.UTF8.GetBytes(jwt.SecretKey)),

//                ClockSkew = TimeSpan.Zero
//            };
//    });

builder.Services.AddPermissionPolicies();
//builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//added for allow access react CORS Pollcy
app.UseCors("ReactPolicy");
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//Inserted intial data like user role etc
await DbInitializer.InitializeAsync(app.Services);
app.Run();
