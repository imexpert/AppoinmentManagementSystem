using AppointmentSchedule.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSchedule.Domain.Events
{
    public class AppointmentRegistrationEvent : INotification
    {
        public AppointmentRegistrationEvent(string name, int notificationTypeId, string source, PhoneNumber destination, int appointmentId)
        {

        }
    }
}
