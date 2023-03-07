using VeterinaryClinic.SharedKernel.Bus;

namespace VeterinaryClinic.SharedKernel.IntegrationEvents
{
    public abstract class IntegrationEventMapper : IIntegrationEventMapper
    {
        public IntegrationEventMapper() { }

        public List<IntegrationEvent> Map(IEnumerable<BaseDomainEvent> domainEvents)
        {
            var integrationEvents = domainEvents.Select(domainEvent => MapDomainEvent(domainEvent))
                                                .Where(integrationEvent => integrationEvent != null)
                                                .ToList();

            return integrationEvents;
        }

        protected abstract IntegrationEvent MapDomainEvent<T>(T domainEvent) where T : BaseDomainEvent;
    }
}
