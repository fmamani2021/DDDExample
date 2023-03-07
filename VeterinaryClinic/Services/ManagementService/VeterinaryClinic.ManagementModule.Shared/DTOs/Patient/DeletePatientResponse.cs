using System;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Patient
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