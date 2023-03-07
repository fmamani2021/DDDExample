using System;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Appointment
{
    public class DeleteAppointmentResponse : BaseResponse
    {

        public DeleteAppointmentResponse(Guid correlationId) : base(correlationId)
        {
        }

        public DeleteAppointmentResponse()
        {
        }
    }
}
