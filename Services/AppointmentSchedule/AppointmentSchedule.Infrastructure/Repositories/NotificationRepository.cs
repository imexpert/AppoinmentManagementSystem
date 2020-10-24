using AppointmentSchedule.Domain.Aggregates.AppointmentAggregate;
using AppointmentSchedule.Domain.Aggregates.CountyAggregate;
using AppointmentSchedule.Domain.Aggregates.NotificationAggregate;
using Microsoft.EntityFrameworkCore;
using NVI.DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedule.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppoinmentScheduleContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public NotificationRepository(AppoinmentScheduleContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Notification> GetAsync(int notificationId)
        {
            var appointmet = await _context
                                .Notifications
                                .Include(x => x.Appointment)
                                .FirstOrDefaultAsync(o => o.Id == notificationId);

            if (appointmet == null)
            {
                appointmet = _context
                .Notifications
                .Local
                .FirstOrDefault(o => o.Id == notificationId);
            }

            if (appointmet != null)
            {
                await _context.Entry(appointmet)
                .Reference(i => i.NotificationType).LoadAsync();
            }

            return appointmet;
        }

        public Notification Add(Notification notification)
        {
            return _context.Notifications.Add(notification).Entity;
        }
    }
}
