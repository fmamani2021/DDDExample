using System;

namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Appointments
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
