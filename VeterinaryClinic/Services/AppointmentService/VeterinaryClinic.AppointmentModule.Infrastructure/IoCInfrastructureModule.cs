using Autofac;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using VeterinaryClinic.AppointmentModule.Infrastructure.Data;
using VeterinaryClinic.AppointmentModule.Infrastructure.IntegrationMapper;
using VeterinaryClinic.AppointmentModule.Infrastructure.MessagingRabbit;
using VeterinaryClinic.SharedKernel.Bus;
using VeterinaryClinic.SharedKernel.IntegrationEvents;
using VeterinaryClinic.SharedKernel.Interfaces;

namespace VeterinaryClinic.AppointmentModule.Infrastructure
{
    public class IoCInfrastructureModule : Module
    {
        private readonly IConfiguration _configuration;

        public IoCInfrastructureModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            //var environment = _configuration.GetSection("Environment").Value;

            RegisterEventBus(builder);
            RegisterEFCore(builder);
            RegisterIntegrationEventMapper(builder);
        }

        private static void RegisterIntegrationEventMapper(ContainerBuilder builder)
        {
            //----------------- MAPPER DOMAIN-EVENT TO INTEGRATION-EVENT------------------------------
            builder.RegisterType<AppointmentIntegrationEventMapper>()
                   .As<IAppointmentIntegrationEventMapper>()
                   .SingleInstance();
        }

        private void RegisterEventBus(ContainerBuilder builder)
        {
            var enableBus = _configuration["EventBus:Enable"]!;
            if (bool.Parse(enableBus) == false) return;

            //-----------------REGISTER MESSAGING BUS------------------------------
            builder.Register(serviceProvider =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _configuration["EventBus:Connection"],
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(_configuration["EventBus:UserName"]))
                {
                    factory.UserName = _configuration["EventBus:UserName"];
                }

                if (!string.IsNullOrEmpty(_configuration["EventBus:Password"]))
                {
                    factory.Password = _configuration["EventBus:Password"];
                }

                var retryCount = 5;
                if (!string.IsNullOrEmpty(_configuration["EventBus:RetryCount"]))
                {
                    retryCount = int.Parse(_configuration["EventBus:RetryCount"]);
                }

                var logger = serviceProvider.Resolve<ILogger<DefaultRabbitMQPersistentConnection>>();
                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            })
            .As<IRabbitMQPersistentConnection>()
            .SingleInstance();

            builder.Register(context =>
            {
                var subscriptionClientName = _configuration["EventBus:SubscriptionClientName"];
                var rabbitMQPersistentConnection = context.Resolve<IRabbitMQPersistentConnection>();
                var iLifetimeScope = context.Resolve<ILifetimeScope>();
                var logger = context.Resolve<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = context.Resolve<IEventBusSubscriptionsManager>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(_configuration["EventBus:RetryCount"]))
                {
                    retryCount = int.Parse(_configuration["EventBus:RetryCount"]!);
                }

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger,
                    iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
            })
            .As<IEventBus, EventBusRabbitMQ>()
            .SingleInstance();

            builder.RegisterType<InMemoryEventBusSubscriptionsManager>()
                .As<IEventBusSubscriptionsManager>()
                .SingleInstance();
        }

        private void RegisterEFCore(ContainerBuilder builder)
        {
            var enableBus = _configuration["EventBus:Enable"]!;
            if (bool.Parse(enableBus))
            {
                builder.RegisterType<AppDbContext>()
                    .AsSelf()
                    .InstancePerRequest()
                    .InstancePerLifetimeScope()
                    .WithParameter(new NamedParameter("connectionString", _configuration.GetConnectionString("DefaultConnection")!))
                    .WithParameter(new NamedParameter("environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!))
                    .WithParameter((pi, ctx) => pi.ParameterType == typeof(IMediator) &&
                                                pi.Name == "mediator", (pi, ctx) => ctx.Resolve<IMediator>())
                    .WithParameter((pi, ctx) => pi.ParameterType == typeof(IEventBus) &&
                                                pi.Name == "eventBus", (pi, ctx) => ctx.Resolve<IEventBus>())
                    .WithParameter((pi, ctx) => pi.ParameterType == typeof(IIntegrationEventMapper) &&
                                                pi.Name == "eventMapper", (pi, ctx) => ctx.Resolve<IAppointmentIntegrationEventMapper>());
            }
            else {
                builder.RegisterType<AppDbContext>()
                    .AsSelf()
                    .InstancePerRequest()
                    .InstancePerLifetimeScope()
                    .WithParameter(new NamedParameter("connectionString", _configuration.GetConnectionString("DefaultConnection")!))
                    .WithParameter(new NamedParameter("environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!))
                    .WithParameter((pi, ctx) => pi.ParameterType == typeof(IMediator) &&
                                                pi.Name == "mediator", (pi, ctx) => ctx.Resolve<IMediator>());
            }

            //-----------------  REGISTER SEED BD ----------------------------------
            builder.RegisterType<AppDbContextSeed>().InstancePerLifetimeScope();

            //-----------------  REGISTER EF GENERIC REPOSITORY --------------------
            builder.RegisterGeneric(typeof(EfRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EfRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EfReadRepository<>))
                .As(typeof(IReadRepository<>))
                .InstancePerLifetimeScope();
        }
    }
}
