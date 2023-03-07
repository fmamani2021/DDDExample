using System;

namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Schedules
{
    public class DeleteScheduleResponse : BaseResponse
    {

        public DeleteScheduleResponse(Guid correlationId) : base(correlationId)
        {
        }

        public DeleteScheduleResponse()
        {
        }
    }
}
