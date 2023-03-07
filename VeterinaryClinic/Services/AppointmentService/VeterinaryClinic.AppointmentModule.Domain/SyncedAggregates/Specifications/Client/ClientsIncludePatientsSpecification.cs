using Ardalis.Specification;

namespace VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates.Specifications
{
    public class ClientsIncludePatientsSpecification : Specification<Client>
    {
        public ClientsIncludePatientsSpecification()
        {
            Query
              .Include(client => client.Patients)
              .OrderBy(client => client.FullName);
        }
    }
}
