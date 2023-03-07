using VeterinaryClinic.SharedKernel;

namespace VeterinaryClinic.ManagementModule.Domain.ClientAgregate.DomainEvents
{
    public class ClientUpdatedEvent : BaseDomainEvent
    {
        public ClientUpdatedEvent(Client client)
        {
            ClientUpdated = client;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();

        public Client ClientUpdated { get; private set; }
    }
}