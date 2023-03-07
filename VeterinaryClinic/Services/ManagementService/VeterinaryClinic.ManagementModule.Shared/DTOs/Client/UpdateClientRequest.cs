using VeterinaryClinic.ManagementModule.Shared.DTOs.Patient;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Client
{
    public class UpdateClientRequest : BaseRequest
    {
        public const string Route = "api/clients";
        
        public int ClientId { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Salutation { get; set; }
        public string PreferredName { get; set; }
        public int? PreferredDoctorId { get; set; }
        public IList<PatientDto> Patients { get; set; } = new List<PatientDto>();

        public UpdateClientRequest(int clientId, string fullName, string emailAddress, string salutation, string preferredName, int? preferredDoctorId, IList<PatientDto> patients)
        {
            ClientId = clientId;
            FullName = fullName;
            EmailAddress = emailAddress;
            Salutation = salutation;
            PreferredName = preferredName;
            PreferredDoctorId = preferredDoctorId;
            Patients = patients;
        }

        public static UpdateClientRequest FromDto(ClientDto model)
        {
            return new UpdateClientRequest(
                model.ClientId,
                model.FullName,
                model.EmailAddress,
                model.Salutation,
                model.PreferredName,
                model.PreferredDoctorId,
                model.Patients
            );
        }
    }
}
