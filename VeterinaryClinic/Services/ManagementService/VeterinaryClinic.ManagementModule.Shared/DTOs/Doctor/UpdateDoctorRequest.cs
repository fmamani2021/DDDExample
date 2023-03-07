using VeterinaryClinic.ManagementModule.Shared.DTOs;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Doctor
{
    public class UpdateDoctorRequest : BaseRequest
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
    }
}
