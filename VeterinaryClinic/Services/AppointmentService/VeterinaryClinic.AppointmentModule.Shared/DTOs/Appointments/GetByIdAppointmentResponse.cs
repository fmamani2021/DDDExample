using System;

namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Appointments
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
