using System;
using System.Collections.Generic;
using VeterinaryClinic.ManagementModule.Shared.DTOs;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Patient
{
    public class ListPatientResponse : BaseResponse
    {
        public List<PatientDto> Patients { get; set; } = new List<PatientDto>();

        public int Count { get; set; }

        public ListPatientResponse(Guid correlationId) : base(correlationId)
        {
        }

        public ListPatientResponse()
        {
        }
    }
}