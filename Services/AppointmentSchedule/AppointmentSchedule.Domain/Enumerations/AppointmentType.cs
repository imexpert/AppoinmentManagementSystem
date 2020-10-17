using NVI.DomainBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSchedule.Domain.Enumerations
{
    public class AppointmentType : Enumeration
    {
        public AppointmentType(int id, string name) :
            base(id, name)
        {

        }

        public static AppointmentType PersonelLicence = new AppointmentType(1, "TCKK");
        public static AppointmentType DriverLicence = new AppointmentType(2, "Surucu");
    }
}
