namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Doctors
{
    public class GetByIdDoctorRequest : BaseRequest
    {
        public const string Route = "api/doctors/{DoctorId}";
        public int DoctorId { get; set; }
    }
}
