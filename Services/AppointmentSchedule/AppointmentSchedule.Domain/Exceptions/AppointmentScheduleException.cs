using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSchedule.Domain.Exceptions
{
    public class AppointmentScheduleException : Exception
    {
        public AppointmentScheduleException()
        { }

        public AppointmentScheduleException(string message)
            : base(message)
        { }

        public AppointmentScheduleException(string message, Exception innerException)
            : base(message, innerException)
        { }

    }
}
