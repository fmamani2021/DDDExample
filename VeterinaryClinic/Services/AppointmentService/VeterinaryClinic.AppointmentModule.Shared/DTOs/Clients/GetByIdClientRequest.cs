namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Clients
{
    public class GetByIdClientRequest : BaseRequest
    {
        public const string Route = "api/clients/{ClientId}";

        public int ClientId { get; set; }
    }
}
