using System;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Client
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
