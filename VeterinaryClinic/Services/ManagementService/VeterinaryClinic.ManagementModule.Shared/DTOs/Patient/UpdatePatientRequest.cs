namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Patient
{
    public class UpdatePatientRequest : BaseRequest
    {
        public int ClientId { get; set; }
        public int PatientId { get; set; }
        public string Name { get; set; }
    }
}
