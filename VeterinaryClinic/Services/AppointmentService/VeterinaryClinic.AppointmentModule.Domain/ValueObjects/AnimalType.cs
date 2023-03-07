using VeterinaryClinic.SharedKernel.ValueObjects;

namespace VeterinaryClinic.AppointmentModule.Domain.ValueObjects
{
    public class AnimalType : ValueObject
    {
        public string Species { get; private set; }
        public string Breed { get; private set; }

        public AnimalType(string species, string breed)
        {
            Species = species;
            Breed = breed;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Breed;
            yield return Species;
        }
    }
}
