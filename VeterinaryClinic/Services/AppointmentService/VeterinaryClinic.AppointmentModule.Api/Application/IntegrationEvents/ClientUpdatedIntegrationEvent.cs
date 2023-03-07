using VeterinaryClinic.SharedKernel.Bus;

namespace VeterinaryClinic.AppointmentModule.Api.Application.IntegrationEvents
{
    public record ClientUpdatedIntegrationEvent : IntegrationEvent
    {
        public ClientUpdatedIntegrationEvent(int clientId, string clientName)
        {
            ClientId = clientId;
            ClientName = clientName;
        }

        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string EventType => nameof(ClientUpdatedIntegrationEvent);
    }
}
