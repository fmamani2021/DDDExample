using System;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Schedule
{
    public class GetByIdScheduleRequest : BaseRequest
    {
        public Guid ScheduleId { get; set; }
    }
}
