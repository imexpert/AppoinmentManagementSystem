using NVI.DomainBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSchedule.Domain.Enumerations
{
    public class StatusType : Enumeration
    {
        public StatusType(int id, string name) :
            base(id, name)
        {

        }

        public static StatusType Active = new StatusType(1, "Aktif");
        public static StatusType Passive = new StatusType(2, "Pasif");
    }
}
