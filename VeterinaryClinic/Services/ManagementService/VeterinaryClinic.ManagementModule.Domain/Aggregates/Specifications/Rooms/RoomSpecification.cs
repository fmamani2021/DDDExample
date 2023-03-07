using Ardalis.Specification;
using VeterinaryClinic.ManagementModule.Domain.Aggregates;

namespace VeterinaryClinic.ManagementModule.Domain.Aggregates.Specifications.Rooms
{
    public class RoomSpecification : Specification<Room>
    {
        public RoomSpecification()
        {
            Query.OrderBy(room => room.Name);
        }
    }
}
