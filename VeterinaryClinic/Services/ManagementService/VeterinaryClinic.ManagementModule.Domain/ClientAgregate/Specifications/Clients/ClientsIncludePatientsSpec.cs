using Ardalis.Specification;
using VeterinaryClinic.ManagementModule.Domain.ClientAgregate;

namespace VeterinaryClinic.ManagementModule.Domain.ClientAgregate.Specifications.Clients
{
    public class ClientsIncludePatientsSpec : Specification<Client>
    {
        public ClientsIncludePatientsSpec()
        {
            Query
              .Include(client => client.Patients)
              .OrderBy(client => client.FullName);
        }
    }
}
