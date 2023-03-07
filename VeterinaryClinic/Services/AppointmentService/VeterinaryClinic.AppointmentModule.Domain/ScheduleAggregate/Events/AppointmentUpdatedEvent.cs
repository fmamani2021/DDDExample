using VeterinaryClinic.SharedKernel;

namespace VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate.Events
{
    public class AppointmentUpdatedEvent : BaseDomainEvent
    {
        public AppointmentUpdatedEvent(Appointment appointment)
        {
            AppointmentUpdated = appointment;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public Appointment AppointmentUpdated { get; private set; }
    }
}
