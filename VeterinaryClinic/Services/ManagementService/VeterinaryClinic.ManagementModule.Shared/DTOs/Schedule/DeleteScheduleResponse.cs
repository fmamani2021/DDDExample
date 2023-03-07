using System;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Schedule
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
