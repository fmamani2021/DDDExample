namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Patient
{
    public class GetByIdPatientRequest : BaseRequest
    {
        public int ClientId { get; set; }
        public int PatientId { get; set; }
    }
}
