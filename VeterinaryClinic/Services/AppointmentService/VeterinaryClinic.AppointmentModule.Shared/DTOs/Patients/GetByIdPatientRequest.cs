namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Patients
{
    public class GetByIdPatientRequest : BaseRequest
    {
        public const string Route = "api/patients/{PatientId}/byclient/{ClientId}";
        public int ClientId { get; set; }
        public int PatientId { get; set; }
    }
}
