using System;

namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Patients
{
    public class DeletePatientResponse : BaseResponse
    {

        public DeletePatientResponse(Guid correlationId) : base(correlationId)
        {
        }

        public DeletePatientResponse()
        {
        }
    }
}