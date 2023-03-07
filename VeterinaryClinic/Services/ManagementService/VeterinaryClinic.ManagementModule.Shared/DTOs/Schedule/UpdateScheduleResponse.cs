using System;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Schedule
{
    public class UpdateScheduleResponse : BaseResponse
    {
        public ScheduleDto Schedule { get; set; } = new ScheduleDto();

        public UpdateScheduleResponse(Guid correlationId) : base(correlationId)
        {
        }

        public UpdateScheduleResponse()
        {
        }
    }
}
