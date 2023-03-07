using VeterinaryClinic.SharedKernel.ValueObjects;

namespace VeterinaryClinic.ManagementModule.Domain.ValueObjects
{
    public class AnimalType : ValueObject
    {
        public string Species { get; private set; }
        public string Breed { get; private set; }

        public AnimalType()
        {

        }
        public AnimalType(string species, string breed)
        {
            Species = species;
            Breed = breed;
        }

        public static AnimalType Create(string species, string breed)
        {
            return new AnimalType(species, breed);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Breed;
            yield return Species;
        }
    }
}
