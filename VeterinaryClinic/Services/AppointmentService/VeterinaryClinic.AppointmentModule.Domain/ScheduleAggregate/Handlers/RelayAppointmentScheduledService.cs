using MediatR;
using Microsoft.Extensions.Logging;
using VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate.Events;

namespace VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate.Handlers
{
    /// <summary>
    /// Post CreateConfirmationEmailMessage to message bus/queue to allow confirmation emails to be sent
    /// </summary>
    public class RelayAppointmentScheduledHandler : 
        INotificationHandler<AppointmentScheduledEvent>
    {
        private readonly ILogger<RelayAppointmentScheduledHandler> _logger;

        public RelayAppointmentScheduledHandler(
          ILogger<RelayAppointmentScheduledHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(
            AppointmentScheduledEvent appointmentScheduledEvent,
          CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling AppointmentScheduledEvent");

            _logger.LogInformation("Apply changes to other Aggregate Root");
            _logger.LogInformation("Apply integration event");            
            _logger.LogInformation("Send Email");
            _logger.LogInformation("Notify UI with some notification mecanism");
        }
    }
}
