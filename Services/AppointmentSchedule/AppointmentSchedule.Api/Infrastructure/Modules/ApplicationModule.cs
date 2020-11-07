using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using AppointmentSchedule.Api.Business.Commands;
using AppointmentSchedule.Api.Business.Queries;
using AppointmentSchedule.Domain.Aggregates.AppointmentAggregate;
using AppointmentSchedule.Infrastructure.Idempotency;
using AppointmentSchedule.Infrastructure.Repositories;
using Autofac;
using NVI.EventBus.Abstractions;

namespace AppointmentSchedule.Api.Infrastructure.Modules
{
    public class ApplicationModule
        :Autofac.Module
    {

        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;

        }

        protected override void Load(ContainerBuilder builder)
        {


            builder.RegisterType<AppointmentScheduleQueries>()
                .As<IAppointmentScheduleQueries>()
                .InstancePerLifetimeScope();


            builder.RegisterType<AppointmentRepository>()
                .As<IAppointmentRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CountyRepository>()
                .As<ICountyRepository>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<NotificationRepository>()
                .As<INotificationRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RequestManager>()
                .As<IRequestManager>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(CreateAppointmentScheduleCommandHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

        }
    }
}
