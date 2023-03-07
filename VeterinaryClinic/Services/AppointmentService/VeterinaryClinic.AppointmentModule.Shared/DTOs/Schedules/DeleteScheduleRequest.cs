using System;

namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Schedules
{
    public class DeleteScheduleRequest : BaseRequest
    {
        public const string Route = "api/schedules/{Id}";
        public Guid Id { get; set; }
    }
}
