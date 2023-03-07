using Ardalis.Specification;
using VeterinaryClinic.ManagementModule.Domain.ClientAgregate;

namespace VeterinaryClinic.ManagementModule.Domain.ClientAgregate.Specifications.Clients
{
    public class ClientByIdIncludePatientsSpec : Specification<Client>, ISingleResultSpecification
    {
        public ClientByIdIncludePatientsSpec(int clientId)
        {
            Query
              .Include(client => client.Patients)
              .Where(client => client.Id == clientId);
        }
    }
}
