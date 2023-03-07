using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VeterinaryClinic.ManagementModule.Domain.Aggregates;
using VeterinaryClinic.ManagementModule.Domain.ClientAgregate;
using VeterinaryClinic.SharedKernel.Bus;
using VeterinaryClinic.SharedKernel.IntegrationEvents;

namespace VeterinaryClinic.ManagementModule.Infrastructure.Data
{
    public class AppDbContext : DbContextBase<AppDbContext>
    {
        //CONTRUCTOR EF POWER TOOL
        public AppDbContext() : base()
        {
        }

        //CONTRUCTOR FOR DOCKER COMPOSE
        public AppDbContext(string connectionString, string environment, IMediator mediator, IEventBus eventBus, IIntegrationEventMapper eventMapper) :
            base(connectionString, environment, mediator, eventBus, eventMapper)
        {
        }

        //CONTRUCTOR FOR DEVELOP
        public AppDbContext(string connectionString, string environment, IMediator mediator) :
            base(connectionString, environment, mediator, null, null)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<AppointmentType> AppointmentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
