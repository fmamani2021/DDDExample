using System;
using System.Collections.Generic;
using VeterinaryClinic.ManagementModule.Shared.DTOs;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Doctor
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