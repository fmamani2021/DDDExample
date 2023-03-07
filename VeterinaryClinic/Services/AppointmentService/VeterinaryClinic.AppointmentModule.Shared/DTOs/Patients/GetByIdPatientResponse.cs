using System;

namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Patients
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