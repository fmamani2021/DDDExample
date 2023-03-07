using VeterinaryClinic.ManagementModule.Domain.ClientAgregate.DomainEvents;
using VeterinaryClinic.ManagementModule.Domain.ClientAgregate.IntegrationEvents;
using VeterinaryClinic.SharedKernel.Bus;
using VeterinaryClinic.SharedKernel.IntegrationEvents;

namespace VeterinaryClinic.ManagementModule.Infrastructure.IntegrationMapper
{
    public class ManagementIntegrationEventMapper : IntegrationEventMapper, IManagementIntegrationEventMapper
    {
        protected override IntegrationEvent MapDomainEvent<T>(T domainEvent)
        {
            return domainEvent switch
            {
                ClientUpdatedEvent @event => new ClientUpdatedIntegrationEvent(@event.ClientUpdated.Id, @event.ClientUpdated.FullName),
                { } => null
            };
        }
    }
}
