using Microsoft.AspNetCore.SignalR;

namespace VeterinaryClinic.AppointmentModule.Api.Notification
{
    public class ScheduleHub : Hub
    {
        public Task UpdateScheduleAsync(string message)
        {
            // TODO: Avoid having messages appear to the user initiating them
            return Clients.Others.SendAsync("ReceiveMessage", message);
        }
    }
}