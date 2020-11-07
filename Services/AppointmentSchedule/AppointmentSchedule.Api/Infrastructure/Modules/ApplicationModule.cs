using System.Reflection;
using AppointmentSchedule.Domain.Aggregates.AppointmentAggregate;
using AppointmentSchedule.Infrastructure.Idempotency;
using AppointmentSchedule.Infrastructure.Repositories;
using Autofac;

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

            builder.Register(c => new OrderQueries(QueriesConnectionString))
                .As<IOrderQueries>()
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

            builder.RegisterAssemblyTypes(typeof(CreateOrderCommandHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

        }
    }
}
