
using AppointmentSchedule.Domain.Enumerations;
using AppointmentSchedule.Domain.ValueObjects;
using NVI.DomainBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSchedule.Domain.Aggregates.NotificationAggregate
{
    public class Notification : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        private int _notificationTypeId;
        public NotificationType NotificationType { get; private set; }
        public string Source { get; private set; }
        public PhoneNumber Destination { get; private set; }
        private int _appointmentId;

        public Notification(string name, int notificationTypeId, string source, PhoneNumber destination, int appointmentId)
        {
            Name = name;
            _notificationTypeId = notificationTypeId;
            Source = source;
            Destination = destination;
            _appointmentId = appointmentId;
        }
    }
}
