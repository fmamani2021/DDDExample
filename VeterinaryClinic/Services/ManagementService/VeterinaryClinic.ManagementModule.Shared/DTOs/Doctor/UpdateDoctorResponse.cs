using System;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Doctor
{
    public class UpdateDoctorResponse : BaseResponse
    {
        public DoctorDto Doctor { get; set; } = new DoctorDto();

        public UpdateDoctorResponse(Guid correlationId) : base(correlationId)
        {
        }

        public UpdateDoctorResponse()
        {
        }
    }
}