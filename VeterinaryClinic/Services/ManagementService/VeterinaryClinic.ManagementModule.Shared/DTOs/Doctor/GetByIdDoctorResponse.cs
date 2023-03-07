using System;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Doctor
{
    public class GetByIdDoctorResponse : BaseResponse
    {
        public DoctorDto Doctor { get; set; } = new DoctorDto();

        public GetByIdDoctorResponse(Guid correlationId) : base(correlationId)
        {
        }

        public GetByIdDoctorResponse()
        {
        }
    }
}