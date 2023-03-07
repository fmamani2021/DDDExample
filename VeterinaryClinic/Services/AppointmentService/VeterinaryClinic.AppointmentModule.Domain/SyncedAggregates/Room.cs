using VeterinaryClinic.SharedKernel;
using VeterinaryClinic.SharedKernel.Interfaces;

namespace VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates
{
    public class Room : BaseEntity<int>, IAggregateRoot
    {
        public Room(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; private set; }

        public override string ToString()
        {
            return Name.ToString();
        }

        public Room UpdateName(string name)
        {
            Name = name;
            return this;
        }
    }
}
