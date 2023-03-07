using Ardalis.Specification;
using VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates;

namespace VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates.Specifications
{
    public class ClientByIdIncludePatientsSpecification : Specification<Client>, ISingleResultSpecification
    {
        public ClientByIdIncludePatientsSpecification(int clientId)
        {
            Query
              .Include(client => client.Patients)
              .Where(client => client.Id == clientId);
        }
    }
}
