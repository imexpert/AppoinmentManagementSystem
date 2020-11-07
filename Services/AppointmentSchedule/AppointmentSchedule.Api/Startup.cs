using AppointmentSchedule.Api.Extensions;
using AppointmentSchedule.Api.Infrastructure.Modules;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace AppoinmentSchedule.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //This method gets called by the runtime.Use this method to add services to the container.
        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddGrpc(options =>
                {
                    options.EnableDetailedErrors = true;
                })
                .Services
                .AddAppointmentScheduleMvc()
                .AddHealthChecks(Configuration)
                .AddAppoinmentScheduleDbContext(Configuration)
                .AddAppoinmentScheduleSwagger(Configuration)
                .AddCustomConfiguration(Configuration);

            //configure autofac

            var container = new ContainerBuilder();
            container.Populate(services);

            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new ApplicationModule(Configuration["ConnectionString"]));

            return new AutofacServiceProvider(container.Build());
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }



}




//public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
//{
//    //loggerFactory.AddAzureWebAppDiagnostics();
//    //loggerFactory.AddApplicationInsights(app.ApplicationServices, LogLevel.Trace);

//    var pathBase = Configuration["PATH_BASE"];
//    if (!string.IsNullOrEmpty(pathBase))
//    {
//        loggerFactory.CreateLogger<Startup>().LogDebug("Using PATH BASE '{pathBase}'", pathBase);
//        app.UsePathBase(pathBase);
//    }

//    app.UseSwagger()
//       .UseSwaggerUI(c =>
//       {
//           c.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "Ordering.API V1");
//           c.OAuthClientId("orderingswaggerui");
//           c.OAuthAppName("Ordering Swagger UI");
//       });

//    app.UseRouting();
//    app.UseCors("CorsPolicy");
//    ConfigureAuth(app);

//    app.UseEndpoints(endpoints =>
//    {
//        endpoints.MapGrpcService<OrderingService>();
//        endpoints.MapDefaultControllerRoute();
//        endpoints.MapControllers();
//        endpoints.MapGet("/_proto/", async ctx =>
//        {
//            ctx.Response.ContentType = "text/plain";
//            using var fs = new FileStream(Path.Combine(env.ContentRootPath, "Proto", "basket.proto"), FileMode.Open, FileAccess.Read);
//            using var sr = new StreamReader(fs);
//            while (!sr.EndOfStream)
//            {
//                var line = await sr.ReadLineAsync();
//                if (line != "/* >>" || line != "<< */")
//                {
//                    await ctx.Response.WriteAsync(line);
//                }
//            }
//        });
//        endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
//        {
//            Predicate = _ => true,
//            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
//        });
//        endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
//        {
//            Predicate = r => r.Name.Contains("self")
//        });
//    });

//    ConfigureEventBus(app);
//}


//private void ConfigureEventBus(IApplicationBuilder app)
//{
//    var eventBus = app.ApplicationServices.GetRequiredService<BuildingBlocks.EventBus.Abstractions.IEventBus>();

//    eventBus.Subscribe<UserCheckoutAcceptedIntegrationEvent, IIntegrationEventHandler<UserCheckoutAcceptedIntegrationEvent>>();
//    eventBus.Subscribe<GracePeriodConfirmedIntegrationEvent, IIntegrationEventHandler<GracePeriodConfirmedIntegrationEvent>>();
//    eventBus.Subscribe<OrderStockConfirmedIntegrationEvent, IIntegrationEventHandler<OrderStockConfirmedIntegrationEvent>>();
//    eventBus.Subscribe<OrderStockRejectedIntegrationEvent, IIntegrationEventHandler<OrderStockRejectedIntegrationEvent>>();
//    eventBus.Subscribe<OrderPaymentFailedIntegrationEvent, IIntegrationEventHandler<OrderPaymentFailedIntegrationEvent>>();
//    eventBus.Subscribe<OrderPaymentSucceededIntegrationEvent, IIntegrationEventHandler<OrderPaymentSucceededIntegrationEvent>>();
//}

//protected virtual void ConfigureAuth(IApplicationBuilder app)
//{
//    if (Configuration.GetValue<bool>("UseLoadTest"))
//    {
//        app.UseMiddleware<ByPassAuthMiddleware>();
//    }

//    app.UseAuthentication();
//    app.UseAuthorization();
//}
//    }

//    static class CustomExtensionsMethods
//{
//    public static IServiceCollection AddApplicationInsights(this IServiceCollection services, IConfiguration configuration)
//    {
//        services.AddApplicationInsightsTelemetry(configuration);
//        services.AddApplicationInsightsKubernetesEnricher();

//        return services;
//    }









//    public static IServiceCollection AddCustomIntegrations(this IServiceCollection services, IConfiguration configuration)
//    {
//        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//        services.AddTransient<IIdentityService, IdentityService>();
//        services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
//            sp => (DbConnection c) => new IntegrationEventLogService(c));

//        services.AddTransient<IOrderingIntegrationEventService, OrderingIntegrationEventService>();

//        if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
//        {
//            services.AddSingleton<IServiceBusPersisterConnection>(sp =>
//            {
//                var logger = sp.GetRequiredService<ILogger<DefaultServiceBusPersisterConnection>>();

//                var serviceBusConnectionString = configuration["EventBusConnection"];
//                var serviceBusConnection = new ServiceBusConnectionStringBuilder(serviceBusConnectionString);

//                return new DefaultServiceBusPersisterConnection(serviceBusConnection, logger);
//            });
//        }
//        else
//        {
//            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
//            {
//                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();


//                var factory = new ConnectionFactory()
//                {
//                    HostName = configuration["EventBusConnection"],
//                    DispatchConsumersAsync = true
//                };

//                if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
//                {
//                    factory.UserName = configuration["EventBusUserName"];
//                }

//                if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
//                {
//                    factory.Password = configuration["EventBusPassword"];
//                }

//                var retryCount = 5;
//                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
//                {
//                    retryCount = int.Parse(configuration["EventBusRetryCount"]);
//                }

//                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
//            });
//        }

//        return services;
//    }



//    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
//    {
//        var subscriptionClientName = configuration["SubscriptionClientName"];

//        if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
//        {
//            services.AddSingleton<IEventBus, EventBusServiceBus>(sp =>
//            {
//                var serviceBusPersisterConnection = sp.GetRequiredService<IServiceBusPersisterConnection>();
//                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
//                var logger = sp.GetRequiredService<ILogger<EventBusServiceBus>>();
//                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

//                return new EventBusServiceBus(serviceBusPersisterConnection, logger,
//                    eventBusSubcriptionsManager, subscriptionClientName, iLifetimeScope);
//            });
//        }
//        else
//        {
//            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
//            {
//                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
//                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
//                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
//                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

//                var retryCount = 5;
//                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
//                {
//                    retryCount = int.Parse(configuration["EventBusRetryCount"]);
//                }

//                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
//            });
//        }

//        services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

//        return services;
//    }

//    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
//    {
//        // prevent from mapping "sub" claim to nameidentifier.
//        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

//        var identityUrl = configuration.GetValue<string>("IdentityUrl");

//        services.AddAuthentication(options =>
//        {
//            options.DefaultAuthenticateScheme = AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
//            options.DefaultChallengeScheme = AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;

//        }).AddJwtBearer(options =>
//        {
//            options.Authority = identityUrl;
//            options.RequireHttpsMetadata = false;
//            options.Audience = "orders";
//        });

//        return services;
//    }