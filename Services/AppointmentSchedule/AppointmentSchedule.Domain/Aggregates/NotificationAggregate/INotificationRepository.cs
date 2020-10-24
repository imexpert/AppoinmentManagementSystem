using AppointmentSchedule.Domain.Aggregates.CountyAggregate;
using AppointmentSchedule.Domain.Aggregates.NotificationAggregate;
using NVI.DomainBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedule.Domain.Aggregates.AppointmentAggregate
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Notification Add(Notification notification);

        Task<Notification> GetAsync(int notificationId);
    }
}
