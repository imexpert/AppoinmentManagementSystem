using AppointmentSchedule.Domain.Aggregates.CountyAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSchedule.Infrastructure.EntityConfigurations
{
    public class CountyConfiguration : IEntityTypeConfiguration<County>
    {
        public void Configure(EntityTypeBuilder<County> countyConfiguration)
        {
            countyConfiguration.ToTable("Counties", AppoinmentScheduleContext.DEFAULT_SCHEMA);

            countyConfiguration
                .HasKey(s => s.Id);

            countyConfiguration
                .Property(s => s.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
