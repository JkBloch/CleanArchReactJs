using EmployeeManagement.API.Authorization;
using EmployeeManagement.Application;
using EmployeeManagement.Application.Validators;
using EmployeeManagement.Infrastructure;
using EmployeeManagement.Infrastructure.Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


QuestPDF.Settings.License = LicenseType.Community;

//Added all service interface of application
builder.Services.AddApplication();

//Added all repository interface of intfrastructure (database)
builder.Services.AddInfrastructure(builder.Configuration);
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
builder.Services.AddAuthorization();
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
app.UseAuthorization();

app.MapControllers();

//Inserted intial data like user role etc
await DbInitializer.InitializeAsync(app.Services);
app.Run();
