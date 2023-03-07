using MediatR;
using Microsoft.AspNetCore.SignalR;
using VeterinaryClinic.AppointmentModule.Api.Notification;
using VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate.Events;

namespace VeterinaryClinic.AppointmentModule.Api.Application.DomainEventHandlers
{
    public class AppointmentScheduledHandler : INotificationHandler<AppointmentScheduledEvent>
    {
        private readonly IHubContext<ScheduleHub> _hubContext;

        public AppointmentScheduledHandler(IHubContext<ScheduleHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task Handle(AppointmentScheduledEvent notification, CancellationToken cancellationToken)
        {
            return _hubContext.Clients.All.SendAsync("ReceiveMessage", notification.AppointmentScheduled.Title + " was JUST SCHEDULED", cancellationToken: cancellationToken);
        }
    }
}
