using AppoinmentSchedule.Domain.Aggregates.AppointmentAggregate;
using AppointmentSchedule.Domain.Enumerations;
using AppointmentSchedule.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSchedule.Domain.Events
{
    public class AppointmentCancellationEvent : INotification
    {
        public string Content { get; }
        public int NotificationTypeId { get; }
        public string Source { get; }
        public PhoneNumber Destination { get; }
        public Appointment Appointment { get; }
        public AppointmentCancellationEvent(Appointment appointment, string content, string source, PhoneNumber destination)
        {
            Appointment = appointment;
            Content = content;
            NotificationTypeId = NotificationType.SMS.Id;
            Source = source;
            Destination = destination;
        }
    }
}
