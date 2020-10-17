using NVI.DomainBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSchedule.Domain.Enumerations
{
    public class NotificationType : Enumeration
    {
        public NotificationType(int id, string name) :
            base(id, name)
        {

        }

        public static NotificationType EGM = new NotificationType(1, "EGM");
        public static NotificationType SMS = new NotificationType(2, "SMS");
    }
}
}
