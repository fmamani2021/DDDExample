using System;

namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Schedules
{
    public class GetByIdScheduleRequest : BaseRequest
    {
        public const string Route = "api/schedules/{scheduleId}";
        public Guid ScheduleId { get; set; }
    }
}
