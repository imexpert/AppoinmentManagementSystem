using AppointmentSchedule.Domain.ValueObjects;
using NVI.DomainBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSchedule.Domain.Aggregates.AppoinmentAggregate
{
    public class Citizen : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Lastname { get; private set; }
        public string TcIdentity { get; set; }
        public PhoneNumber PhoneNumber { get; private set; }

        protected Citizen()
        { }

        public Citizen(string name, string lastname, string tcIdentity, PhoneNumber phoneNumber)
        {
            Name = name;
            Lastname = lastname;
            PhoneNumber = phoneNumber;
            TcIdentity = tcIdentity;
        }
    }
}
