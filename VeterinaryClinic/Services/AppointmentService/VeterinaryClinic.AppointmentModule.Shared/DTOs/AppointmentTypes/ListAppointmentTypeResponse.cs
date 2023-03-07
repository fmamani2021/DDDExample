using System;
using System.Collections.Generic;

namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.AppointmentTypes
{
    public class ListAppointmentTypeResponse : BaseResponse
    {
        public List<AppointmentTypeDto> AppointmentTypes { get; set; } = new List<AppointmentTypeDto>();

        public int Count { get; set; }

        public ListAppointmentTypeResponse(Guid correlationId) : base(correlationId)
        {
        }

        public ListAppointmentTypeResponse()
        {
        }
    }
}
