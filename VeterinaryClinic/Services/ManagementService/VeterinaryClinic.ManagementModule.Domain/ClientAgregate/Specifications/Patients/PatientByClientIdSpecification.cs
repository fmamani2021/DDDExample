using Ardalis.Specification;
using VeterinaryClinic.ManagementModule.Domain.ClientAgregate;

namespace VeterinaryClinic.ManagementModule.Domain.ClientAgregate.Specifications.Patients
{
    public class PatientByClientIdSpecification : Specification<Patient>
    {
        public PatientByClientIdSpecification(int clientId)
        {
            Query
                .Where(patient => patient.ClientId == clientId);

            Query.OrderBy(patient => patient.Name);
        }
    }
}
