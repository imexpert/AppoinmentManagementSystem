﻿using AppoinmentSchedule.Domain.Aggregates.AppointmentAggregate;
using AppointmentSchedule.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSchedule.Domain.Events
{
    public class AppointmentRegistrationEvent : INotification
    {
        public string Name { get; }
        public int NotificationTypeId { get; }
        public string Source { get; }
        public PhoneNumber Destination { get; }
        public Appointment Appointment { get; }
        public AppointmentRegistrationEvent(Appointment appointment, string name, int notificationTypeId, string source, PhoneNumber destination)
        {
            Appointment = appointment;
            Name = name;
            NotificationTypeId = notificationTypeId;
            Source = source;
            Destination = destination;
        }
    }
}
