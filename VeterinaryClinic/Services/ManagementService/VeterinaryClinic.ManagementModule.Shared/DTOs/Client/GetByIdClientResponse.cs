using System;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Client
{
    public class GetByIdClientResponse : BaseResponse
    {
        public ClientDto Client { get; set; } = new ClientDto();

        public GetByIdClientResponse(Guid correlationId) : base(correlationId)
        {
        }

        public GetByIdClientResponse()
        {
        }
    }
}
