using System;

namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Doctors
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