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

    public class CreateAppointmentScheduleCommandHandler : IRequestHandler<CreateAppointmentScheduleCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IRequestManager _requestManager;
        private readonly ILogger<CreateAppointmentScheduleCommandHandler> _logger;

        public CreateAppointmentScheduleCommandHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<CreateAppointmentScheduleCommandHandler> logger)
        {
            _mediator = mediator;
            _requestManager = requestManager;
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }



        public async Task<bool> Handle(CreateAppointmentScheduleCommand createAppointmentScheduleCommand, CancellationToken cancellationToken)
        {



            return await Task.FromResult(true);

        }
    }

}
