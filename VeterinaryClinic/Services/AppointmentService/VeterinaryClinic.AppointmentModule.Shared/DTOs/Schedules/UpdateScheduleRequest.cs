using System;
using System.Collections.Generic;

namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Schedules
{
    public class UpdateScheduleRequest : BaseRequest
    {
        public Guid Id { get; set; }
        public int ClinicId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        private List<Guid> AppointmentIds { get; set; } = new List<Guid>();
    }
}
