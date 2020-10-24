using AppointmentSchedule.Domain.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSchedule.Infrastructure.EntityConfigurations
{
    public class NotificationTypeConfiguration : IEntityTypeConfiguration<NotificationType>
    {
        public void Configure(EntityTypeBuilder<NotificationType> notificationTypeConfiguration)
        {
            notificationTypeConfiguration.ToTable("NotificationTypes", AppoinmentScheduleContext.DEFAULT_SCHEMA);

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
