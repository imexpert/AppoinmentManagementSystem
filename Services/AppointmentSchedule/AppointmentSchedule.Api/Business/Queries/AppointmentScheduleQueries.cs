using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSchedule.Domain.Aggregates.AppointmentAggregate;
using AppointmentSchedule.Infrastructure;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace AppointmentSchedule.Api.Business.Queries
{
    public class AppointmentScheduleQueries : IAppointmentScheduleQueries
    {

        private AppoinmentScheduleContext _appoinmentScheduleContext;

        public AppointmentScheduleQueries(AppoinmentScheduleContext appoinmentScheduleContext)
        {
            _appoinmentScheduleContext = appoinmentScheduleContext ?? throw new ArgumentNullException(nameof(AppoinmentScheduleContext));
        }

        public async Task<IEnumerable<AppointmentViewModel>> GetAppointmentsAsync(string tcIdentity, int pageIndex = 0, int pageSize = 10)
        {
            var appointmentResult = await GetAppointments()
               .Where(k => k.TcIdentity == tcIdentity)
               .Skip(pageSize * pageIndex)
               .Take(pageSize)
               .ToListAsync();
            return appointmentResult;
        }

        public async Task<AppointmentViewModel> GetAppointmentDetailAsync(int appointmentId)
        {
            var appointmentResult = await GetAppointments()
                .FirstOrDefaultAsync(k => k.Id == appointmentId);
            return appointmentResult;
        }

        public async Task<IEnumerable<AppointmentViewModel>> GetAppointmentsByDateAsync(string tcIdentity, DateTime appointmentDate)
        {
            var appointmentResult = await GetAppointments()
              .Where(k => k.TcIdentity == tcIdentity)
              .ToListAsync();
            return appointmentResult;
        }

        private IQueryable<AppointmentViewModel> GetAppointments()
        {
            var appointmentResult = _appoinmentScheduleContext.Appointments
              .AsQueryable()
              .Include(i => i.Citizen)
              .Include(i => i.Status)
              .Join(_appoinmentScheduleContext.Counties,
              app => app.CountyId,
              city => city.Id,
              (app, city) => new AppointmentViewModel
              {
                  AppoinmentTime = app.AppoinmentTime,
                  AppointmentType = app.AppointmentType.Name,
                  Id = app.Id,
                  Name = app.Citizen.Name,
                  Lastname = app.Citizen.Name,
                  TcIdentity = app.Citizen.TcIdentity,
                  PhoneNumber = app.Citizen.PhoneNumber.ToString(),
                  CityName = city.Name
              });
            return appointmentResult;

        }
    }
}
