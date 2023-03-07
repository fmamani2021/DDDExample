using System;
using VeterinaryClinic.ManagementModule.Shared.DTOs;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Patient
{
    public class GetByIdPatientResponse : BaseResponse
    {
        public PatientDto Patient { get; set; } = new PatientDto();

        public GetByIdPatientResponse(Guid correlationId) : base(correlationId)
        {
        }

        public GetByIdPatientResponse()
        {
        }
    }
}