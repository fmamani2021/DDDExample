using Ardalis.Specification;
using VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates;

namespace VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates.Specifications
{
    public class RoomSpecification : Specification<Room>
    {
        public RoomSpecification()
        {
            Query.OrderBy(room => room.Name);
        }
    }
}
