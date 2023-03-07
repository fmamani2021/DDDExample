using System;

namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Clients
{
    public class DeleteClientResponse : BaseResponse
    {

        public DeleteClientResponse(Guid correlationId) : base(correlationId)
        {
        }

        public DeleteClientResponse()
        {
        }
    }
}
