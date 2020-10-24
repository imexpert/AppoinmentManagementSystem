using AppointmentSchedule.Domain.Aggregates.AppointmentAggregate;
using AppointmentSchedule.Domain.Aggregates.CountyAggregate;
using Microsoft.EntityFrameworkCore;
using NVI.DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedule.Infrastructure.Repositories
{
    public class CountyRepository : ICountyRepository
    {
        private readonly AppoinmentScheduleContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public CountyRepository(AppoinmentScheduleContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<County> FindByIdAsync(int countyId)
        {
            var appointmet = await _context
                                .Counties
                                .FirstOrDefaultAsync(o => o.Id == countyId);

            return appointmet;
        }
    }
}
