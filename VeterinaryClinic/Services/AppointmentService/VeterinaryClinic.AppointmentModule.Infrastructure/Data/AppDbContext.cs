using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate;
using VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates;
using VeterinaryClinic.SharedKernel.Bus;
using VeterinaryClinic.SharedKernel.IntegrationEvents;

namespace VeterinaryClinic.AppointmentModule.Infrastructure.Data
{
    public class AppDbContext : DbContextBase<AppDbContext>
    {
        //CONTRUCTOR EF POWER TOOL
        public AppDbContext() : base()
        {
        }

        //CONTRUCTOR FOR DEVELOP
        public AppDbContext(string connectionString, string environment, IMediator mediator) :
            base(connectionString, environment, mediator)
        {
        }

        //CONTRUCTOR FOR DOCKER COMPOSE
        public AppDbContext(string connectionString, string environment, IMediator mediator, IEventBus eventBus, IIntegrationEventMapper eventMapper) :
            base(connectionString, environment, mediator, eventBus, eventMapper)
        {
        }

        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentType> AppointmentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
