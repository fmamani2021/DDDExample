using VeterinaryClinic.ManagementModule.Shared.DTOs;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Patient
{
    public class DeletePatientRequest : BaseRequest
    {
        public int ClientId { get; set; }
        public int PatientId { get; set; }
    }
}
