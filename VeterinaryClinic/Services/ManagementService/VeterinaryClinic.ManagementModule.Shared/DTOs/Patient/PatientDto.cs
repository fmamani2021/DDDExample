using VeterinaryClinic.ManagementModule.Shared.Common;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Patient
{
    public class PatientDto
    {
        public int ClientId { get; set; }
        public int PatientId { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public int? PreferredDoctorId { get; set; }

        public string Status { get; set; } = ModelStatus.Unchange;
    }
}
