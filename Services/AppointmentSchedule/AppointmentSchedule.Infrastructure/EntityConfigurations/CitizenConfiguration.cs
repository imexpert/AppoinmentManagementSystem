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
            citizenConfiguration.ToTable("Citizens", AppointmentScheduleContext.DEFAULT_SCHEMA);

            citizenConfiguration.HasKey(a => a.Id);

            citizenConfiguration.Property(a => a.Id).UseHiLo("citizenseq", AppointmentScheduleContext.DEFAULT_SCHEMA);

            citizenConfiguration
                .Property(a => a.Name)
                .HasMaxLength(100)
                .IsRequired();

            citizenConfiguration
                .Property(a => a.Lastname)
                .HasMaxLength(100)
                .IsRequired();

            citizenConfiguration
                .Property(a => a.TcIdentity)
                .HasMaxLength(11)
                .IsRequired();

            citizenConfiguration
                .HasIndex(a => a.TcIdentity)
                .IsUnique(true);


            citizenConfiguration
             .OwnsOne(a => a.PhoneNumber, o =>
             {
                 o.WithOwner();
             });


        }
    }
}
