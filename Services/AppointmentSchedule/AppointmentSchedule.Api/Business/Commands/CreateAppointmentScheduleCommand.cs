using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AppointmentSchedule.Api.Business.Commands
{
    [DataContract]
    public class CreateAppointmentScheduleCommand : IRequest<bool>
    {
        [DataMember]
        public DateTime AppointmentTime { get; private set; }

        [DataMember]
        public int AppointmentTypeId { get; private set; }

        [DataMember]
        public int CountyId { get; private set; }

        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public string Lastname { get; private set; }

        [DataMember]
        public string TcIdentity { get; private set; }

        [DataMember]
        public string PhoneNumber { get; private set; }


        public CreateAppointmentScheduleCommand()
        {
        }

        public CreateAppointmentScheduleCommand(
            DateTime appointmentTime,
            int appointmentTypeId,
            int countyId,
            string name,
            string lastname,
            string tcIdentity,
            string phoneNumber) : this()
        {
            AppointmentTime = appointmentTime;
            AppointmentTypeId = appointmentTypeId;
            CountyId = countyId;
            Name = name;
            Lastname = lastname;
            TcIdentity = tcIdentity;
            PhoneNumber = phoneNumber;
        }
    }
}
