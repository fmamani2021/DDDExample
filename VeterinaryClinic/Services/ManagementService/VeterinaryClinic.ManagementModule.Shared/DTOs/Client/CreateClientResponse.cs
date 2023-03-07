using System;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Client
{
    public class CreateClientResponse : BaseResponse
    {
        public ClientDto Client { get; set; } = new ClientDto();

        public CreateClientResponse(Guid correlationId) : base(correlationId)
        {
        }

        public CreateClientResponse()
        {
        }
    }
}
