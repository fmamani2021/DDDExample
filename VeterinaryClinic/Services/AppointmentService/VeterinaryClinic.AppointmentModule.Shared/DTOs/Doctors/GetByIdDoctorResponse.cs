using System;

namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Doctors
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