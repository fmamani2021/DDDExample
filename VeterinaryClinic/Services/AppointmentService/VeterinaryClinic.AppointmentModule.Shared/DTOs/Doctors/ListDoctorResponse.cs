using System;
using System.Collections.Generic;

namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Doctors
{
    public class ListDoctorResponse : BaseResponse
    {
        public List<DoctorDto> Doctors { get; set; } = new List<DoctorDto>();

        public int Count { get; set; }

        public ListDoctorResponse(Guid correlationId) : base(correlationId)
        {
        }

        public ListDoctorResponse()
        {
        }
    }
}