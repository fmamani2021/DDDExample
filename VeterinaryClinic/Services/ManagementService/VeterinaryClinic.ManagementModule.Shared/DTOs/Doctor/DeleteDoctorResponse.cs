using System;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Doctor
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