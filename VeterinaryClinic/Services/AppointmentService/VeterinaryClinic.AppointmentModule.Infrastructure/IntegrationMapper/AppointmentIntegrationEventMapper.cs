using VeterinaryClinic.SharedKernel.Bus;
using VeterinaryClinic.SharedKernel.IntegrationEvents;

namespace VeterinaryClinic.AppointmentModule.Infrastructure.IntegrationMapper
{
    public class AppointmentIntegrationEventMapper : IntegrationEventMapper, IAppointmentIntegrationEventMapper
    {
        protected override IntegrationEvent MapDomainEvent<T>(T domainEvent)
        {
            return domainEvent switch
            {
                { } => null
            };
        }
    }
}
