using System;
using VeterinaryClinic.ManagementModule.Shared.DTOs;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Appointment
{
    public class CreateAppointmentResponse : BaseResponse
    {
        public AppointmentDto Appointment { get; set; } = new AppointmentDto();

        public CreateAppointmentResponse(Guid correlationId) : base(correlationId)
        {
        }

        public CreateAppointmentResponse()
        {
        }
    }
}