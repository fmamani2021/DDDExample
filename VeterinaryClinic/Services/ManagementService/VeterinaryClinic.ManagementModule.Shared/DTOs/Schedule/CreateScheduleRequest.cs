﻿using System;
using System.Collections.Generic;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Schedule
{
    public class CreateScheduleRequest : BaseRequest
    {
        public int ClinicId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        private List<Guid> AppointmentIds { get; set; } = new List<Guid>();
    }
}
