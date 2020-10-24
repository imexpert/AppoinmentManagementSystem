using AppointmentSchedule.Domain.Aggregates.AppoinmentAggregate;
using AppointmentSchedule.Domain.Enumerations;
using AppointmentSchedule.Domain.ValueObjects;
using NVI.DomainBase;
using System;
using System.Collections.Generic;
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
        private int _cityId;

        private int _statusId;
        public StatusType Status { get; private set; }

        protected Appointment()
        {
            
        }

        public Appointment(
            DateTime appoinmentTime, 
            int appointmentTypeId, 
            int cityId, 
            string name, 
            string lastname, 
            string tcIdentity,
            PhoneNumber phoneNumber)
        {
            AppoinmentTime = appoinmentTime;
            _appointmentTypeId = appointmentTypeId;
            _statusId = StatusType.Active.Id;
            _cityId = cityId;

            _citizen = new Citizen(name, lastname, tcIdentity, phoneNumber);

            //AddDomainEvent()
        }

        public void CancelAppointment()
        {
            _statusId = StatusType.Passive.Id;
        }
    }
}
