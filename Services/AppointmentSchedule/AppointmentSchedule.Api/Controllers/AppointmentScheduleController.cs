using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AppointmentSchedule.Api.Business.Commands;
using AppointmentSchedule.Api.Business.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NVI.Utilities.Extensions;

namespace AppoinmentSchedule.Api.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class AppointmentScheduleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAppointmentScheduleQueries _appointmentScheduleQueries;
        private readonly ILogger<AppointmentScheduleController> _logger;

        public AppointmentScheduleController(
            ILogger<AppointmentScheduleController> logger,
            IMediator mediator,
            IAppointmentScheduleQueries appointmentScheduleQueries)
        {
            _logger = logger;
            _appointmentScheduleQueries = appointmentScheduleQueries;
            _mediator = mediator;
        }

        [Route("{apppointmentId:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(AppointmentViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<AppointmentViewModel>> GetAppointmentAsync(int apppointmentId)
        {
            try
            {
                var appointment = await _appointmentScheduleQueries.GetAppointmentDetailAsync(apppointmentId);
                return Ok(appointment);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("bytcidentity")]
        [HttpPost]
        [ProducesResponseType(typeof(AppointmentViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<AppointmentViewModel>>> GetAppointmentsByTcIdentityAsync(AppointmentQueryDto appointmentQueryDto)
        {
            try
            {
                var appointments = await _appointmentScheduleQueries.GetAppointmentsAsync(appointmentQueryDto.TcIdentity, appointmentQueryDto.PageIndex, appointmentQueryDto.PageSize);

                return Ok(appointments);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("bydate")]
        [HttpPost]
        [ProducesResponseType(typeof(AppointmentViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<AppointmentViewModel>>> GetAppointmentsByDateAsync(AppointmentQueryDto appointmentQueryDto)
        {
            try
            {
                var appointments = await _appointmentScheduleQueries.GetAppointmentsByDateAsync(appointmentQueryDto.TcIdentity, appointmentQueryDto.Date);

                return Ok(appointments);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("cancel")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CancelOrderAsync([FromBody]CancelAppointmentScheduleCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;

            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var requestCancelAppointment = new IdentifiedCommand<CancelAppointmentScheduleCommand, bool>(command, guid);

                _logger.LogInformation(
                    "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestCancelAppointment.GetGenericTypeName(),
                    nameof(requestCancelAppointment.Command.AppointmentId),
                    requestCancelAppointment.Command.AppointmentId,
                    requestCancelAppointment);

                commandResult = await _mediator.Send(requestCancelAppointment);
            }

            if (!commandResult)
            {
                return BadRequest();
            }

            return Ok();
        }


        [Route("ship")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ShipOrderAsync([FromBody]CreateAppointmentScheduleCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;

            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var requestCreateAppointment = new IdentifiedCommand<CreateAppointmentScheduleCommand, bool>(command, guid);

                _logger.LogInformation(
                    "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestCreateAppointment.GetGenericTypeName(),
                    nameof(requestCreateAppointment.Command.TcIdentity),
                    requestCreateAppointment.Command.TcIdentity,
                    requestCreateAppointment);

                commandResult = await _mediator.Send(requestCreateAppointment);
            }

            if (!commandResult)
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}
