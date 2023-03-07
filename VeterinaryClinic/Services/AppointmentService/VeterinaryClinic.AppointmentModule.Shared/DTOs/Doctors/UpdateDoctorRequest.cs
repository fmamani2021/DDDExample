namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Doctors
{
    public class UpdateDoctorRequest : BaseRequest
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
    }
}
