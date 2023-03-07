using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using VeterinaryClinic.AppointmentModule.Api.Notification;
using VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate;
using VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate.Events;
using VeterinaryClinic.SharedKernel.ValueObjects.Custom;

namespace VeterinaryClinic.AppointmentModule.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<ScheduleHub> _hubContext;

        public TestController(IMediator mediator, IHubContext<ScheduleHub> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;           
        }

        [HttpGet("DomainEvent")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var dateRange = DateTimeOffsetRange.Create(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddHours(1));
            var appointment = new Appointment(
                Guid.NewGuid(), 1, Guid.NewGuid(), 1, 1, 1, 1, dateRange, "test", DateTime.Now);
            var appointmentScheduledEvent = new AppointmentScheduledEvent(appointment);
            await _mediator.Publish(appointmentScheduledEvent).ConfigureAwait(false);

            return Ok();
        }

        [HttpGet("SignalNotificaion")]
        public async Task<IActionResult> Notify(CancellationToken cancellationToken)
        {
            var notificationRequest = new
            {
                code = "client-updated",
                message = $"The client name OLDNAME was updated with the value NEWNAME."
            };
            string notification = JsonSerializer.Serialize(notificationRequest);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", notification);
            return Ok();
        }
    }
}
