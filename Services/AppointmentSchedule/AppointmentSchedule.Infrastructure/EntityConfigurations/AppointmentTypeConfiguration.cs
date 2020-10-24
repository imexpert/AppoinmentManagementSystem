using AppoinmentSchedule.Domain.Aggregates.AppointmentAggregate;
using AppointmentSchedule.Domain.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSchedule.Infrastructure.EntityConfigurations
{
    public class AppointmentTypeConfiguration : IEntityTypeConfiguration<AppointmentType>
    {
        public void Configure(EntityTypeBuilder<AppointmentType> appointmentTypeConfiguration)
        {
            appointmentTypeConfiguration.ToTable("AppointmentTypes", AppoinmentScheduleContext.DEFAULT_SCHEMA);

            appointmentTypeConfiguration
                .HasKey(s => s.Id);

            appointmentTypeConfiguration
                .Property(s => s.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            appointmentTypeConfiguration
                .Property(s => s.Name)
                .HasMaxLength(200)
                .IsRequired();

        }
    }
}
