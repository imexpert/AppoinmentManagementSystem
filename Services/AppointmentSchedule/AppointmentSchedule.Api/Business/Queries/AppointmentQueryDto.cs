using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSchedule.Api.Business.Queries
{
    public class AppointmentQueryDto
    {
        public string TcIdentity { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public DateTime Date { get; set; }

    }
}
