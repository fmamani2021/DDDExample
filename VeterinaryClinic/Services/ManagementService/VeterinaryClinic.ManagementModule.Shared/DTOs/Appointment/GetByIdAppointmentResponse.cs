using System;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Appointment
{
    public class GetByIdAppointmentResponse : BaseResponse
    {
        public AppointmentDto Appointment { get; set; } = new AppointmentDto();

        public GetByIdAppointmentResponse(Guid correlationId) : base(correlationId)
        {
        }

        public GetByIdAppointmentResponse()
        {
        }
    }
}
