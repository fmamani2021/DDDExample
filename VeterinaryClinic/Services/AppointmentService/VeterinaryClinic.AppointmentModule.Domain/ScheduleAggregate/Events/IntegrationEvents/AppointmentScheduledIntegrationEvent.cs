using VeterinaryClinic.SharedKernel.Bus;

namespace VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate.Events.IntegrationEvents
{
    public record AppointmentScheduledIntegrationEvent : IntegrationEvent
    {
        public AppointmentScheduledIntegrationEvent()
        {
            
        }

        public Guid AppointmentId { get; set; }
        public string ClientName { get; set; }
        public string ClientEmailAddress { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string AppointmentType { get; set; }
        public DateTimeOffset AppointmentStartDateTime { get; set; }
        public string EventType => nameof(AppointmentScheduledIntegrationEvent);
    }
}
