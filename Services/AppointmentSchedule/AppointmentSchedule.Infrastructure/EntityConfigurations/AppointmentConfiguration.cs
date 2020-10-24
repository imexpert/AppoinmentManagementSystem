﻿using AppoinmentSchedule.Domain.Aggregates.AppointmentAggregate;
using AppointmentSchedule.Domain.Aggregates.CountyAggregate;
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

            appointmentConfiguration.Property<int>("_cityId")
                .UsePropertyAccessMode(PropertyAccessMode.Property)
                .HasColumnName("CityId")
                .IsRequired();

            appointmentConfiguration
                .HasOne<County>()
                .WithMany()
                .IsRequired()
                .HasForeignKey("_cityId");

            appointmentConfiguration
                .HasOne(s => s.Citizen)
                .WithMany()
                .IsRequired()
                .HasForeignKey("_citizenId");

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
