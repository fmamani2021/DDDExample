using System;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Schedule
{
    public class CreateScheduleResponse : BaseResponse
    {
        public ScheduleDto Schedule { get; set; } = new ScheduleDto();

        public CreateScheduleResponse(Guid correlationId) : base(correlationId)
        {
        }

        public CreateScheduleResponse()
        {
        }
    }
}