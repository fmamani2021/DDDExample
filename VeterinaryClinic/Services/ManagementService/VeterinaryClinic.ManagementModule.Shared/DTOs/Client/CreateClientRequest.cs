using VeterinaryClinic.ManagementModule.Shared.DTOs.Patient;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Client
{
    public class CreateClientRequest : BaseRequest
    {
        public const string Route = "api/clients";

        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Salutation { get; set; }
        public string PreferredName { get; set; }
        public int? PreferredDoctorId { get; set; }
        public IList<PatientDto> Patients { get; set; } = new List<PatientDto>();

        public CreateClientRequest(string fullName, string emailAddress, string salutation, string preferredName, int? preferredDoctorId, IList<PatientDto> patients)
        {
            FullName = fullName;
            EmailAddress = emailAddress;
            Salutation = salutation;
            PreferredName = preferredName;
            PreferredDoctorId = preferredDoctorId;
            Patients = patients;
        }

        public static CreateClientRequest FromDto(ClientDto model)
        {
            return new CreateClientRequest(
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
