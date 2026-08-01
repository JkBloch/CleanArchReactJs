using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Infrastructure.Data.Configurations
{
    //public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    //{
    //    public void Configure(EntityTypeBuilder<Employee> builder)
    //    {
    //        builder.ToTable("Employees");

    //        builder.HasKey(x => x.Id);

    //        builder.Property(x => x.EmployeeCode)
    //            .HasMaxLength(20)
    //            .IsRequired();

    //        builder.Property(x => x.FirstName)
    //            .HasMaxLength(100)
    //            .IsRequired();

    //        builder.Property(x => x.LastName)
    //            .HasMaxLength(100)
    //            .IsRequired();

    //        builder.Property(x => x.Email)
    //            .HasMaxLength(150)
    //            .IsRequired();

    //        builder.HasIndex(x => x.Email)
    //            .IsUnique();

    //        builder.Property(x => x.Department)
    //            .HasMaxLength(100);

    //        builder.Property(x => x.PhoneNumber)
    //            .HasMaxLength(20);

    //        builder.Property(x => x.Salary)
    //            .HasColumnType("decimal(18,2)");
    //    }
    //}
}
