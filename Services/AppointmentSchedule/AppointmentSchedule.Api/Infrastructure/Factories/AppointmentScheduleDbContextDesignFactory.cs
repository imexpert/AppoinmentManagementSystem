using AppointmentSchedule.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSchedule.Api.Infrastructure.Factories
{
    public class AppointmentScheduleDbContextDesignFactory : IDesignTimeDbContextFactory<AppoinmentScheduleContext>
    {
        public AppoinmentScheduleContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppoinmentScheduleContext>();

            optionsBuilder.UseSqlServer(config["ConnectionString"], sqlServerOptionsAction: o => o.MigrationsAssembly("AppointmentSchedule.Api"));

            return new AppoinmentScheduleContext(optionsBuilder.Options);
        }
    }
}
