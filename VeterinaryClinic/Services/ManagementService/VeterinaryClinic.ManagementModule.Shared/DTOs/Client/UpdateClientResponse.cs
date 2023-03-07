namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Client
{
    public class UpdateClientResponse : BaseResponse
    {
        public ClientDto Client { get; set; } = new ClientDto();

        public UpdateClientResponse(Guid correlationId) : base(correlationId)
        {
        }

        public UpdateClientResponse()
        {
        }
    }
}
