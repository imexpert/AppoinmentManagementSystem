using AppoinmentSchedule.Domain.Aggregates.AppointmentAggregate;
using AppointmentSchedule.Domain.Aggregates.CountyAggregate;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSchedule.Infrastructure.EntityConfigurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> appointmentConfiguration)
        {
            appointmentConfiguration.ToTable("Appointments", AppoinmentScheduleContext.DEFAULT_SCHEMA);

            appointmentConfiguration.HasKey(s => s.Id);
            appointmentConfiguration.Ignore(s => s.DomainEvents);
            appointmentConfiguration
                .Property(s => s.Id)
                .UseHiLo("appointmentseq", AppoinmentScheduleContext.DEFAULT_SCHEMA);

            //
            appointmentConfiguration.Property(s => s.AppoinmentTime)
                .UsePropertyAccessMode(PropertyAccessMode.Property)
                .HasColumnName("AppoinmentTime")
                .IsRequired();

            //appointmentConfiguration.Property(b => b.CountyId).HasField("_countyId");

            appointmentConfiguration.Property<int>("_countyId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("CountyId")
                .IsRequired();

            appointmentConfiguration
                .HasOne<County>()
                .WithMany()
                .IsRequired()
                .HasForeignKey("_countyId");

            //appointmentConfiguration.Metadata
            //    .FindNavigation(nameof(Appointment.Citizen))
            //    .SetPropertyAccessMode(PropertyAccessMode.Field);

            appointmentConfiguration
                .HasOne(s => s.Citizen)
                .WithMany()
                .IsRequired()
                .HasForeignKey("CitizenId");

            appointmentConfiguration.Property<int>("_statusId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("StatusId")
                .IsRequired();

            appointmentConfiguration
                .HasOne(s => s.Status)
                .WithMany()
                .IsRequired()
                .HasForeignKey("_statusId");

            appointmentConfiguration.Property<int>("_appointmentTypeId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("AppointmentTypeId")
                .IsRequired();

            appointmentConfiguration
                .HasOne(s => s.AppointmentType)
                .WithMany()
                .IsRequired()
                .HasForeignKey("_appointmentTypeId");
        }
    }
}
