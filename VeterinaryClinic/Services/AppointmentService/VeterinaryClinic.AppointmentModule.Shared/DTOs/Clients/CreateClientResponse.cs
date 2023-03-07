using System;

namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Clients
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
