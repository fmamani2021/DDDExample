using VeterinaryClinic.SharedKernel.Bus;

namespace VeterinaryClinic.SharedKernel.IntegrationEvents
{
    public interface IIntegrationEventMapper
    {
        List<IntegrationEvent> Map(IEnumerable<BaseDomainEvent> domainEvents);
    }
}
