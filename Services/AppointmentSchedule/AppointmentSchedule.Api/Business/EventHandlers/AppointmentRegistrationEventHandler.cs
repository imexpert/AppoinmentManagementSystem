using AppointmentSchedule.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AppointmentSchedule.Api.Business.EventHandlers
{
    public class AppointmentRegistrationEventHandler : INotificationHandler<AppointmentRegistrationEvent>
    {
        private readonly ILoggerFactory _logger;

        public AppointmentRegistrationEventHandler(ILoggerFactory logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(
            AppointmentRegistrationEvent appointmentCancellationEvent,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
