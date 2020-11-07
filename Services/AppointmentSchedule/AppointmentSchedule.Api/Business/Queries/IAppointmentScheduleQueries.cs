using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentSchedule.Api.Business.Queries
{
    public interface IAppointmentScheduleQueries
    {
        Task<AppointmentViewModel> GetAppointmentDetailAsync(int appointmentId);

        Task<IEnumerable<AppointmentViewModel>> GetAppointmentsAsync(string tcIdentity, int pageIndex = 0, int pageSize = 10);

        Task<IEnumerable<AppointmentViewModel>> GetAppointmentsByDateAsync(string tcIdentity,DateTime appointmentDate);

    }
}
