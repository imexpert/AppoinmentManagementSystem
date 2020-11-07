using System;

namespace AppointmentSchedule.Api.Business.Queries
{

    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public DateTime AppoinmentTime { get; set; }
        public string AppointmentType { get; set; }
        public string CityName { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string TcIdentity { get; set; }
        public string PhoneNumber { get; set; }

    }
}
