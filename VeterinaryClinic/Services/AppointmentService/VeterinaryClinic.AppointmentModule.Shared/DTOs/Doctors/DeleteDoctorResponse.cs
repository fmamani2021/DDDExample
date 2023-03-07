using System;

namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Doctors
{
    public class DeleteDoctorResponse : BaseResponse
    {

        public DeleteDoctorResponse(Guid correlationId) : base(correlationId)
        {
        }

        public DeleteDoctorResponse()
        {
        }
    }
}