using Ardalis.GuardClauses;
using VeterinaryClinic.ManagementModule.Domain.ClientAgregate.DomainEvents;
using VeterinaryClinic.ManagementModule.Domain.ValueObjects;
using VeterinaryClinic.SharedKernel;
using VeterinaryClinic.SharedKernel.Interfaces;

namespace VeterinaryClinic.ManagementModule.Domain.ClientAgregate
{
    public class Client : BaseEntity<int>, IAggregateRoot
    {
        public string FullName { get; set; }
        public string PreferredName { get; set; }
        public string Salutation { get; set; }
        public string EmailAddress { get; set; }
        public int PreferredDoctorId { get; set; }
        public IList<Patient> Patients { get; private set; } = new List<Patient>();

        private Client() { }

        public Client(string fullName,
          string preferredName,
          string salutation,
          int preferredDoctorId,
          string emailAddress)
        {
            FullName = fullName;
            PreferredName = preferredName;
            Salutation = salutation;
            PreferredDoctorId = preferredDoctorId;
            EmailAddress = emailAddress;
        }

        public override string ToString()
        {
            return FullName.ToString();
        }

        public void UpdateName(string fullName)
        {

            FullName = fullName;

            AddDomainEvent(new ClientUpdatedEvent(this));
            //AddDomainEvent(new ClientUpdatedEvent(this));
        }

        public void CreatePatient(int clientId, string name, string sex,
            string species, string breed, int? preferredDoctorId)
        {
            //Apply validations

            var animalType = AnimalType.Create(species, breed);
            Patients.Add(Patient.Create(clientId, name, sex, animalType, preferredDoctorId));

            //Add some domain events ehere
        }

        public void UpdatePatient(int patientId, string name, string sex, string species, string breed, int? preferredDoctorId)
        {
            //Apply validations 

            var patient = Patients.FirstOrDefault(p => p.Id == patientId);
            if (patient == null)
                throw new NotFoundException(nameof(DeletePatient), $"Patient with ID: {patientId} not found");

            var animalType = AnimalType.Create(species, breed);
            patient.Update(name, sex, animalType, preferredDoctorId);
            
        }

        public void DeletePatient(int patientId)
        {
            //Apply validations 

            var patient = Patients.FirstOrDefault(p => p.Id == patientId);
            if (patient == null)
                throw new NotFoundException(nameof(DeletePatient), $"Patient with ID: {patientId} not found");

            Patients.Remove(patient);

            //Add some domain events ehere

        }

    }
}
