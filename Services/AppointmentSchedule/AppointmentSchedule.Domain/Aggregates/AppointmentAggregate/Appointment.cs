using AppointmentSchedule.Domain.Aggregates.AppoinmentAggregate;
using AppointmentSchedule.Domain.Enumerations;
using AppointmentSchedule.Domain.Events;
using AppointmentSchedule.Domain.ValueObjects;
using NVI.DomainBase;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace AppoinmentSchedule.Domain.Aggregates.AppointmentAggregate
{
    public class Appointment : Entity, IAggregateRoot
    {
        public DateTime AppoinmentTime { get; private set; }

        private readonly Citizen _citizen;
        public Citizen Citizen => _citizen;

        private int _appointmentTypeId;
        public AppointmentType AppointmentType { get; private set; }
        
        private int _countyId;
        
        private int _statusId;
        public StatusType Status { get; private set; }

        protected Appointment()
        {
            
        }

        public Appointment(
            DateTime appoinmentTime, 
            int appointmentTypeId, 
            int countyId, 
            string name, 
            string lastname, 
            string tcIdentity,
            PhoneNumber phoneNumber)
        {
            AppoinmentTime = appoinmentTime;
            _appointmentTypeId = appointmentTypeId;
            _statusId = StatusType.Active.Id;
            _countyId = countyId;

            _citizen = new Citizen(name, lastname, tcIdentity, phoneNumber);

            this.AddDomainEvent(
                new AppointmentRegistrationEvent(
                    this, 
                    "Randevunuz oluşturulmuştur. ", 
                    NotificationType.SMS.Id,
                    Assembly.GetEntryAssembly().GetName().Name, 
                    phoneNumber));

            this.AddDomainEvent(
                new AppointmentRegistrationEvent(
                    this,
                    "Randevunuz oluşturulmuştur. ",
                    NotificationType.EGM.Id,
                    Assembly.GetEntryAssembly().GetName().Name,
                    phoneNumber));
        }

        public void CancelAppointment()
        {
            _statusId = StatusType.Passive.Id;

            this.AddDomainEvent(
                new AppointmentCancellationEvent(
                    this,
                    "Randevunuz iptal edilmiştir. ",
                    Assembly.GetEntryAssembly().GetName().Name,
                    this.Citizen.PhoneNumber));
        }
    }
}
