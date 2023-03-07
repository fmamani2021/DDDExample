using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Threading;
using VeterinaryClinic.AppointmentModule.Api.Application.IntegrationEvents;
using VeterinaryClinic.AppointmentModule.Api.Notification;
using VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate;
using VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates;
using VeterinaryClinic.SharedKernel.Bus;
using VeterinaryClinic.SharedKernel.Interfaces;

namespace VeterinaryClinic.AppointmentModule.Api.Application.IntegrationEventHandlers
{
    public record ClientUpdatedIntegrationEventHandler : IIntegrationEventHandler<ClientUpdatedIntegrationEvent>
    {
        private readonly ILogger<ClientUpdatedIntegrationEventHandler> _logger;
        private readonly IHubContext<ScheduleHub> _hubContext;
        private readonly IRepository<Client> _clientRepository;

        public ClientUpdatedIntegrationEventHandler(
            ILogger<ClientUpdatedIntegrationEventHandler> logger,
            IHubContext<ScheduleHub> hubContext,
            IRepository<Client> clientRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hubContext = hubContext;
            _clientRepository = clientRepository;
        }

        public async Task Handle(ClientUpdatedIntegrationEvent @event)
        {
            //FOR DEMONSTRATION PURPOSES ONLY UPDATE THE CLIENT AND SEND A NOTIFICATION WITH SIGNALR AT THE SAME TIME
            _logger.LogInformation("----- Handling integration event update-client");

            var client = await _clientRepository.GetByIdAsync(@event.ClientId);
            if (client == null) return;

            var oldName = client.FullName;
            

            client.UpdateFullName(@event.ClientName);

            var notificationRequest = new {
                code = "client-updated",
                message = $"The client name {oldName} was updated with the value {@event.ClientName}."                
            };
            string notification = JsonSerializer.Serialize(notificationRequest);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", notification);
        }
    }
}
