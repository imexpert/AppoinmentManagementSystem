using AppointmentSchedule.Domain.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSchedule.Infrastructure.EntityConfigurations
{
    public class StatusTypeConfiguration : IEntityTypeConfiguration<StatusType>
    {
        public void Configure(EntityTypeBuilder<StatusType> notificationTypeConfiguration)
        {
            notificationTypeConfiguration.ToTable("StatusTypes", AppoinmentScheduleContext.DEFAULT_SCHEMA);

            notificationTypeConfiguration
                .Property(s => s.Id);

            notificationTypeConfiguration
                .HasKey(s => s.Id);

            notificationTypeConfiguration
                .Property(s => s.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            notificationTypeConfiguration
                .Property(s => s.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
