using AppointmentSchedule.Domain.Aggregates.NotificationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSchedule.Infrastructure.EntityConfigurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> notificationConfiguration)
        {
            notificationConfiguration.ToTable("Notifications", AppoinmentScheduleContext.DEFAULT_SCHEMA);

            notificationConfiguration
                .Property(s => s.Id)
                .UseHiLo("notificationseq", AppoinmentScheduleContext.DEFAULT_SCHEMA);

            notificationConfiguration
                .HasKey(s => s.Id);

            notificationConfiguration
                .Property(s => s.Content)
                .HasMaxLength(100)
                .IsRequired();

            notificationConfiguration.Property<int>("_notificationTypeId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("NotificationTypeId")
                .IsRequired();

            notificationConfiguration
                .HasOne(s => s.NotificationType)
                .WithMany()
                .IsRequired()
                .HasForeignKey("_notificationTypeId");

            notificationConfiguration
                .Property(s => s.Source)
                .HasMaxLength(100)
                .IsRequired();

            notificationConfiguration
                .OwnsOne(s => s.Destination, s =>
                {
                    s.WithOwner();
                });

            notificationConfiguration.Property<int>("_appointmentId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("AppointmentId")
                .IsRequired();

            notificationConfiguration
                .HasOne(s => s.Appointment)
                .WithOne()
                .IsRequired()
                .HasForeignKey("_appointmentId");
        }
    }
}
