using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Infrastructure.Data.Configurations.Admin
{
    public class ApplicationLogConfiguration : IEntityTypeConfiguration<ApplicationLog>
    {
        public void Configure(EntityTypeBuilder<ApplicationLog> builder)
        {
            builder.ToTable("ApplicationLogs");
            
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Level)
                .HasMaxLength(50);

            builder.Property(x => x.Message)
                .HasColumnType("nvarchar(max)");

            builder.Property(x => x.Exception)
                .HasColumnType("nvarchar(max)");
        }
    }

}
