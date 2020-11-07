using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSchedule.Api.Business.Commands;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AppointmentSchedule.Api.Business.Validations
{

    public class CreateAppointmentScheduleValidator : AbstractValidator<CreateAppointmentScheduleCommand>
    {
        public CreateAppointmentScheduleValidator(ILogger<CreateAppointmentScheduleValidator> logger)
        {
            RuleFor(command => command.AppointmentTime).NotEmpty().Must(BeValidExpirationDate).WithMessage("Please specify a valid card expiration date");
            RuleFor(command => command.AppointmentTypeId).NotEmpty();
            RuleFor(command => command.CountyId).NotEmpty();
            RuleFor(command => command.Lastname).NotEmpty();
            RuleFor(command => command.Name).NotEmpty();
            RuleFor(command => command.PhoneNumber).NotEmpty();
            RuleFor(command => command.TcIdentity).NotEmpty();


            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        private bool BeValidExpirationDate(DateTime dateTime)
        {
            return dateTime >= DateTime.UtcNow;
        }


    }
}
