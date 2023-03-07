using System;

namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Patients
{
    public class CreatePatientResponse : BaseResponse
    {
        public PatientDto Patient { get; set; } = new PatientDto();

        public CreatePatientResponse(Guid correlationId) : base(correlationId)
        {
        }

        public CreatePatientResponse()
        {
        }
    }
}