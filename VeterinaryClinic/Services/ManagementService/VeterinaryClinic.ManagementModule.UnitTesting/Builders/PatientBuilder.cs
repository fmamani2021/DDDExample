using VeterinaryClinic.ManagementModule.Domain.ClientAgregate;
using VeterinaryClinic.ManagementModule.Domain.ValueObjects;

namespace VeterinaryClinic.ManagementModule.UnitTesting.Builders
{
    public class PatientBuilder
    {
        private Patient _patient;

        public PatientBuilder()
        {
            WithDefaultValues();
        }

        public PatientBuilder Id(int id)
        {
            _patient.Id = id;
            return this;
        }

        public PatientBuilder SetPatient(Patient patient)
        {
            _patient = patient;
            return this;
        }

        public PatientBuilder WithDefaultValues()
        {
            _patient = Patient.Create(
                clientId: 1, 
                name: "Test Patient", 
                sex: "MALE", 
                animalType: AnimalType.Create("Cat", "Mixed"), 
                preferredDoctorId: null
            );            
            return this;
        }

        public Patient Build() => _patient;
    }
}
