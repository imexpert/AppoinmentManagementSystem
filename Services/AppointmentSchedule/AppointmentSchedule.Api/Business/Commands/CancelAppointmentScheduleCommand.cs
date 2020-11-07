using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AppointmentSchedule.Api.Business.Commands
{
    [DataContract]
    public class CancelAppointmentScheduleCommand : IRequest<bool>
    {
        [DataMember]
        public int AppointmentId { get; private set; }

        public CancelAppointmentScheduleCommand()
        {
        }

        public CancelAppointmentScheduleCommand(
           int appointmentId) : this()
        {
            AppointmentId = appointmentId;
        }
    }
}
