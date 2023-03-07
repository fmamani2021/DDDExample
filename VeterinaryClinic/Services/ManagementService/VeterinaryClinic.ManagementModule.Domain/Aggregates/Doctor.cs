using VeterinaryClinic.SharedKernel;
using VeterinaryClinic.SharedKernel.Interfaces;

namespace VeterinaryClinic.ManagementModule.Domain.Aggregates
{
    public class Doctor : BaseEntity<int>, IAggregateRoot
    {
        public string Name { get; set; }

        public Doctor(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
