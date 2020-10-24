using AppoinmentSchedule.Domain.Aggregates.AppointmentAggregate;
using AppointmentSchedule.Domain.Aggregates.NotificationAggregate;
using NVI.DomainBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedule.Domain.Aggregates.AppointmentAggregate
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Appointment Add(Appointment order);
        void Update(Appointment order);

        Task<Appointment> GetAsync(int orderId);
    }
}
