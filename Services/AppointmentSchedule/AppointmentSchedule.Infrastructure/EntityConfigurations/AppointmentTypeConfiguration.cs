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
            appointmentTypeConfiguration.ToTable("AppointmentTypes", AppointmentScheduleContext.DEFAULT_SCHEMA);

            appointmentTypeConfiguration
                .HasKey(a => a.Id);

            appointmentTypeConfiguration
                .Property(a => a.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            appointmentTypeConfiguration
                .Property(a => a.Name)
                .HasMaxLength(200)
                .IsRequired();          
        }
    }
}
