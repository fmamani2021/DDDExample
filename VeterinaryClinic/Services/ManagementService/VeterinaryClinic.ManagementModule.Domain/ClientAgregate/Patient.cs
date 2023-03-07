using VeterinaryClinic.ManagementModule.Domain.ValueObjects;
using VeterinaryClinic.SharedKernel;

namespace VeterinaryClinic.ManagementModule.Domain.ClientAgregate
{
    public class Patient : BaseEntity<int>
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public AnimalType AnimalType { get; set; }
        public int? PreferredDoctorId { get; set; }

        protected Patient() { }


        public static Patient Create(
            int clientId, string name, string sex,
            AnimalType animalType, int? preferredDoctorId)
        {
            //Apply validation or events here

            return new Patient
            {
                ClientId = clientId,
                Name = name,
                Sex = sex,
                AnimalType = animalType,
                PreferredDoctorId = preferredDoctorId
            };
        }

        internal void Update(string name, string sex, AnimalType animalType, int? preferredDoctorId)
        {
            Name = name;
            Sex = sex;
            AnimalType = animalType;
            PreferredDoctorId = preferredDoctorId;
        }
    }
}
