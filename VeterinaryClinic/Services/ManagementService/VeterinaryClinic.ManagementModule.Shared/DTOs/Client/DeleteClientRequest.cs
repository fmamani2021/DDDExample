namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Client
{
    public class DeleteClientRequest : BaseRequest
    {
        public const string Route = "api/clients/{id}";

        public int Id { get; set; }
    }
}
