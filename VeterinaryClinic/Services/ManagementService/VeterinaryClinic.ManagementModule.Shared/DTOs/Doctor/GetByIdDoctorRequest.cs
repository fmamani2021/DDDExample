using VeterinaryClinic.ManagementModule.Shared.DTOs;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Doctor
{
    public class GetByIdDoctorRequest : BaseRequest
    {
        public int DoctorId { get; set; }
    }
}
