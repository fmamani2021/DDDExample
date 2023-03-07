using System;
using VeterinaryClinic.ManagementModule.Shared.DTOs;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Appointment
{
    public class UpdateAppointmentResponse : BaseResponse
    {
        public AppointmentDto Appointment { get; set; } = new AppointmentDto();

        public UpdateAppointmentResponse(Guid correlationId) : base(correlationId)
        {
        }

        public UpdateAppointmentResponse()
        {
        }
    }
}
