using System;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Schedule
{
    public class DeleteScheduleRequest : BaseRequest
    {
        public Guid Id { get; set; }
    }
}
