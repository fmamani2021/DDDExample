using System;

namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Schedules
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
