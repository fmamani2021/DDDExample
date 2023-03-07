using VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates;
using VeterinaryClinic.AppointmentModule.Domain.ValueObjects;

namespace VeterinaryClinic.AppointmentModule.UnitTesting.Builders
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
            _patient = new Patient(1, "Test Patient", "MALE", new AnimalType("Cat", "Mixed"));

            return this;
        }

        public Patient Build() => _patient;
    }
}
