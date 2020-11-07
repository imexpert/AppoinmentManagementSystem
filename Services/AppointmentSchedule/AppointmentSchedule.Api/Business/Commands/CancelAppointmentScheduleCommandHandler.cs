using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AppointmentSchedule.Domain.Exceptions;
using AppointmentSchedule.Infrastructure.Idempotency;
using MediatR;
using Microsoft.Extensions.Logging;
using NVI.Utilities.Extensions;

namespace AppointmentSchedule.Api.Business.Commands
{

    public class CancelAppointmentScheduleCommandHandler : IRequestHandler<CancelAppointmentScheduleCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IRequestManager _requestManager;
        private readonly ILogger<CancelAppointmentScheduleCommandHandler> _logger;

        public CancelAppointmentScheduleCommandHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<CancelAppointmentScheduleCommandHandler> logger)
        {
            _mediator = mediator;
            _requestManager = requestManager;
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CancelAppointmentScheduleCommand createAppointmentScheduleCommand, CancellationToken cancellationToken)
        {

            return await Task.FromResult(true);
        }
    }

}
