namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Client
{
    public class GetByIdClientRequest : BaseRequest
    {
        public const string Route = "api/clients/{ClientId}";

        public int ClientId { get; set; }
    }
}
