using AppointmentSchedule.Domain.Enumerations;
using AppointmentSchedule.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NVI.DomainBase;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NVI.Utilities.Extensions;
using AppointmentSchedule.Domain.Aggregates.CountyAggregate;

namespace AppointmentSchedule.Api.Infrastructure
{
    public class AppoinmentScheduleContextSeed
    {
        public async Task SeedAsync(AppoinmentScheduleContext context, IWebHostEnvironment env, ILogger<AppoinmentScheduleContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(AppoinmentScheduleContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                var contentRootPath = env.ContentRootPath;

                using (context)
                {
                    context.Database.Migrate();

                    if (!context.AppointmentTypes.Any())
                    {
                        context.AppointmentTypes.AddRange(GetPredefinedAppointmentTypes());

                        await context.SaveChangesAsync();
                    }

                    if (!context.NotificationTypes.Any())
                    {
                        context.NotificationTypes.AddRange(GetPredefinedNotificationTypes());

                        await context.SaveChangesAsync();
                    }

                    if (!context.StatusTypes.Any())
                    {
                        context.StatusTypes.AddRange(GetPredefinedStatusTypes());

                        await context.SaveChangesAsync();
                    }

                    if (!context.Counties.Any())
                    {
                        context.Counties.AddRange(GetPredefinedCounties(contentRootPath, logger));

                        await context.SaveChangesAsync();
                    }

                    await context.SaveChangesAsync();
                }
            });
        }

        private IEnumerable<AppointmentType> GetPredefinedAppointmentTypes()
        {
            return Enumeration.GetAll<AppointmentType>();
        }

        private IEnumerable<NotificationType> GetPredefinedNotificationTypes()
        {
            return Enumeration.GetAll<NotificationType>();
        }

        private IEnumerable<StatusType> GetPredefinedStatusTypes()
        {
            return Enumeration.GetAll<StatusType>();
        }

        private IEnumerable<County> GetPredefinedCounties(string contentRootPath, ILogger<AppoinmentScheduleContextSeed> log)
        {
            string csvFileCardTypes = Path.Combine(contentRootPath, "Setup", "Counties.csv");

            if (!File.Exists(csvFileCardTypes))
            {
                throw new FileNotFoundException("Counties.csv");
            }

            string[] csvheaders;

            string[] requiredHeaders = { "Counties" };
            csvheaders = GetHeaders(requiredHeaders, csvFileCardTypes);

            return File.ReadAllLines(csvFileCardTypes)
            .Skip(1) // skip header column
            .SelectTry(x => CreateCountyType(x))
            .OnCaughtException(ex => { log.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message); return null; })
            .Where(x => x != null);
        }

        private County CreateCountyType(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new Exception("County name is null or empty");
            }

            return new County(value.Trim('"').Trim());
        }

        private string[] GetHeaders(string[] requiredHeaders, string csvfile)
        {
            string[] csvheaders = File.ReadLines(csvfile).First().ToLowerInvariant().Split(',');

            if (csvheaders.Count() != requiredHeaders.Count())
            {
                throw new Exception($"requiredHeader count '{ requiredHeaders.Count()}' is different then read header '{csvheaders.Count()}'");
            }

            foreach (var requiredHeader in requiredHeaders)
            {
                if (!csvheaders.Contains(requiredHeader))
                {
                    throw new Exception($"does not contain required header '{requiredHeader}'");
                }
            }

            return csvheaders;
        }

        private AsyncRetryPolicy CreatePolicy(ILogger<AppoinmentScheduleContextSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().
            WaitAndRetryAsync(
            retryCount: retries,
            sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
            onRetry: (exception, timeSpan, retry, ctx) =>
            {
                logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
            }
            );
        }
    }
}
