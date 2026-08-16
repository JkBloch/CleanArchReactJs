using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Entities.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Infrastructure.Data.Configurations.Master
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(150)
                .IsRequired();

            builder.HasIndex(x => x.Email)
                .IsUnique();
            
            builder.Property(x => x.PhoneNumber)
                .IsRequired(false)
                .HasMaxLength(20);

            builder.Property(x => x.Salary)
                .IsRequired(false)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.DateOfBirth) 
                .IsRequired(false);

            builder.Property(x => x.JoiningDate)
                .IsRequired(false);

            builder.Property(x => x.Gender)
                .IsRequired(false); 

            builder.HasOne(x => x.State)
            .WithMany(x => x.Employees)
            .HasForeignKey(x => x.StateId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Department)
            .WithMany(x => x.Employees)
            .HasForeignKey(x => x.DepartmentId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.City)
            .WithMany(x => x.Employees)
            .HasForeignKey(x => x.CityId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
