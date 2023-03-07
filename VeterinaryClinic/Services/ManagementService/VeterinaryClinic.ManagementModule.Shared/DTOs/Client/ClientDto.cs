using VeterinaryClinic.ManagementModule.Shared.DTOs.Patient;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Client
{
    public class ClientDto
    {
        public int ClientId { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Salutation { get; set; }
        public string PreferredName { get; set; }
        public int? PreferredDoctorId { get; set; }
        public IList<PatientDto> Patients { get; set; } = new List<PatientDto>();
    }
}
