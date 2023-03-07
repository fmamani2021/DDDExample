using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VeterinaryClinic.SharedKernel;
using VeterinaryClinic.SharedKernel.Bus;
using VeterinaryClinic.SharedKernel.IntegrationEvents;

namespace VeterinaryClinic.AppointmentModule.Infrastructure.Data
{
    public abstract class DbContextBase<T> : DbContext where T : DbContextBase<T>
    {
        internal readonly string connectionString;
        internal readonly string environment;
        internal readonly IMediator mediator;
        internal readonly IIntegrationEventMapper? eventMapper = null;
        internal readonly IEventBus? eventBus = null;

        internal IIntegrationEventMapper EventMapper => eventMapper;

        protected DbContextBase()
        {
        }

        protected DbContextBase(
            string _connectionString,
            string _environment,
            IMediator _mediator)
        {
            connectionString = _connectionString;
            environment = _environment;
            mediator = _mediator;            
        }


        protected DbContextBase(
            string _connectionString,
            string _environment,
            IMediator _mediator,
            IEventBus _eventBus,
            IIntegrationEventMapper _eventMapper)
        {
            connectionString = _connectionString;
            environment = _environment;
            mediator = _mediator;
            eventMapper = _eventMapper;
            eventBus = _eventBus;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);

            if (environment == "Development" || environment == "Docker")
            {
                optionsBuilder.EnableSensitiveDataLogging()
                    .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                    .EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<BaseDomainEvent>();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await mediator!.DispatchEventsAsync(this);
            // After executing this line all the changes (from the Command Handler and Domain Event Handlers 
            // performed through the DbContext will be committed

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}
