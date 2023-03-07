using System;
using System.Collections.Generic;

namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Appointments
{
    public class ListAppointmentResponse : BaseResponse
    {
        public List<AppointmentDto> Appointments { get; set; } = new List<AppointmentDto>();

        public int Count { get; set; }

        public ListAppointmentResponse(Guid correlationId) : base(correlationId)
        {
        }

        public ListAppointmentResponse()
        {
        }
    }
}
