using VeterinaryClinic.SharedKernel;
using VeterinaryClinic.SharedKernel.Interfaces;

namespace VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates
{
    public class Doctor : BaseEntity<int>, IAggregateRoot
    {
        public Doctor(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; private set; }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
