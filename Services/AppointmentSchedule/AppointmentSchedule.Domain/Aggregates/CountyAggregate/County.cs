using NVI.DomainBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSchedule.Domain.Aggregates.CountyAggregate
{
    public class County : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public County(string name)
        {
            Name = name;
        }
    }
}
