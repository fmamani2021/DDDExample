namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Patients
{
    public class DeletePatientRequest : BaseRequest
    {
        public int ClientId { get; set; }
        public int PatientId { get; set; }
    }
}
