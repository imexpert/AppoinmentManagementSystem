using AppoinmentSchedule.Domain.Aggregates.AppointmentAggregate;
using AppointmentSchedule.Domain.Aggregates.CountyAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSchedule.Infrastructure.EntityConfigurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> appointmenConfiguration)
        {
            appointmenConfiguration.ToTable("Appointment", AppointmentScheduleContext.DEFAULT_SCHEMA);

            appointmenConfiguration.HasKey(a => a.Id);

            appointmenConfiguration.Ignore(a => a.DomainEvents);

            appointmenConfiguration.Property(a => a.Id).UseHiLo("appointmentseq", AppointmentScheduleContext.DEFAULT_SCHEMA);

            // bu maplame işlemine gerek yok. Çünkü property ismi ile sutun ismi aynı ve nullable değil
            appointmenConfiguration
                .Property(a => a.AppoinmentTime)
                .UsePropertyAccessMode(PropertyAccessMode.Property)
                .HasColumnName("AppoinmentTime")
                .IsRequired();

            appointmenConfiguration
                .Property<int>("_cityId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("CityId")
                .IsRequired();

            appointmenConfiguration
                .HasOne<County>()
                .WithMany()
                .IsRequired()
                .HasForeignKey("_cityId");

            appointmenConfiguration
                .HasOne(a => a.Citizen)
                .WithMany()
                .IsRequired()
                .HasForeignKey("_citizenId");

            appointmenConfiguration
                .Property("_statusId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("StatusId")
                .IsRequired();

            appointmenConfiguration
                .HasOne(a => a.Status)
                .WithMany()
                .IsRequired()
                .HasForeignKey("_statusId");

            appointmenConfiguration
              .Property("_appointmentTypeId")
              .UsePropertyAccessMode(PropertyAccessMode.Field)
              .HasColumnName("AppointmentTypeId")
              .IsRequired();

            appointmenConfiguration
                .HasOne(a => a.AppointmentType)
                .WithMany()
                .IsRequired()
                .HasForeignKey("_appointmentTypeId");

        }
    }
}
