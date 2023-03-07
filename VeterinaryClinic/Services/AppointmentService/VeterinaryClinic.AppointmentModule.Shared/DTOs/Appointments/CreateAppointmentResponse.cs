using System;

namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Appointments
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