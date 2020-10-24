using AppointmentSchedule.Domain.Aggregates.CountyAggregate;
using NVI.DomainBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedule.Domain.Aggregates.AppointmentAggregate
{
    public interface ICountyRepository : IRepository<County>
    {
        Task<County> FindByIdAsync(int countyId);
    }
}
