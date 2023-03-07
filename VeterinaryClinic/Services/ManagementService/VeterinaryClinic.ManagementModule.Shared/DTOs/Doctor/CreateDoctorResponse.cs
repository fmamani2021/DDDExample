using System;
using VeterinaryClinic.ManagementModule.Shared.DTOs;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Doctor
{
    public class CreateDoctorResponse : BaseResponse
    {
        public DoctorDto Doctor { get; set; } = new DoctorDto();

        public CreateDoctorResponse(Guid correlationId) : base(correlationId)
        {
        }

        public CreateDoctorResponse()
        {
        }
    }
}