using AppoinmentSchedule.Domain.Aggregates.AppointmentAggregate;
using AppointmentSchedule.Domain.Aggregates.AppointmentAggregate;
using Microsoft.EntityFrameworkCore;
using NVI.DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedule.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppoinmentScheduleContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public AppointmentRepository(AppoinmentScheduleContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Appointment Add(Appointment order)
        {
            return _context.Appointments.Add(order).Entity;
        }

        public async Task<Appointment> GetAsync(int appointmetId)
        {
            var appointmet = await _context
                                .Appointments
                                .Include(x => x.Citizen)
                                .FirstOrDefaultAsync(o => o.Id == appointmetId);

            if (appointmet == null)
            {
                appointmet = _context
                .Appointments
                .Local
                .FirstOrDefault(o => o.Id == appointmetId);
            }

            if (appointmet != null)
            {
                await _context.Entry(appointmet)
                .Reference(i => i.Status).LoadAsync();

                await _context.Entry(appointmet)
                .Reference(i => i.AppointmentType).LoadAsync();
            }

            return appointmet;
        }

        public void Update(Appointment appointment)
        {
            _context.Entry(appointment).State = EntityState.Modified;
        }
    }
}
