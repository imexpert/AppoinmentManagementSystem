using AppointmentSchedule.Domain.Aggregates.AppoinmentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSchedule.Infrastructure.EntityConfigurations
{
    public class CitizenConfiguration : IEntityTypeConfiguration<Citizen>
    {
        public void Configure(EntityTypeBuilder<Citizen> citizenConfiguration)
        {
            citizenConfiguration.ToTable("Citizens", AppoinmentScheduleContext.DEFAULT_SCHEMA);

            citizenConfiguration
                .Property(s => s.Id)
                .UseHiLo("citizenseq", AppoinmentScheduleContext.DEFAULT_SCHEMA);

            citizenConfiguration
                .HasKey(s => s.Id);

            citizenConfiguration
                .Property(s => s.Name)
                .HasMaxLength(100)
                .IsRequired();

            citizenConfiguration
                .Property(s => s.Lastname)
                .HasMaxLength(100)
                .IsRequired();

            citizenConfiguration
                .Property(s => s.TcIdentity)
                .HasMaxLength(11)
                .IsRequired();

            citizenConfiguration.HasIndex(s => s.TcIdentity).IsUnique();

            citizenConfiguration
                .OwnsOne(s => s.PhoneNumber, s => { s.WithOwner(); });
        }
    }
}
