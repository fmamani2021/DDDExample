using AutoFixture;
using AutoFixture.Kernel;
using VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate;
using VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate.Events;
using VeterinaryClinic.SharedKernel.ValueObjects.Custom;
using Xunit;

namespace VeterinaryClinic.AppointmentModule.UnitTesting.Core.ScheduleAggregate.AppointmentTests
{
    public class Appointment_UpdateDoctor
    {
        private readonly Fixture _fixture = new Fixture();
        public Appointment_UpdateDoctor()
        {
            _fixture.Customizations.Add(
              new FilteringSpecimenBuilder(
                new FixedBuilder(DateTimeOffsetRange.Create(DateTime.Today.AddHours(12), DateTime.Today.AddHours(13))),
                new ParameterSpecification(
                  typeof(DateTimeOffsetRange), "timeRange")));
        }

        private Appointment GetUnconfirmedAppointment()
        {
            var newAppt = _fixture.Build<Appointment>()
              .Create();
            newAppt.DateTimeConfirmed = null;
            return newAppt;
        }

        [Fact]
        public void DoesNothingGivenSameDoctorId()
        {
            var appointment = GetUnconfirmedAppointment();
            var initialDoctorId = appointment.DoctorId;
            var initialEventCount = appointment.DomainEvents.Count;

            appointment.UpdateDoctor(initialDoctorId);

            Assert.Equal(initialDoctorId, appointment.DoctorId);
            Assert.Equal(initialEventCount, appointment.DomainEvents.Count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ThrowsGivenNegativeOrZeroDoctorId(int invalidDoctorId)
        {
            var appointment = GetUnconfirmedAppointment();

            Action action = () => appointment.UpdateDoctor(invalidDoctorId);

            Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void UpdatesDoctorIdAndAddsEventGivenNewDoctorId()
        {
            var appointment = GetUnconfirmedAppointment();
            var initialDoctorId = appointment.DoctorId;
            var initialEventCount = appointment.DomainEvents.Count;

            int newDoctorId = initialDoctorId + 1;
            appointment.UpdateDoctor(newDoctorId);

            Assert.Equal(newDoctorId, appointment.DoctorId);
            Assert.Single(appointment.DomainEvents);
            Assert.Contains(appointment.DomainEvents, x => x.GetType() == typeof(AppointmentUpdatedEvent));
        }
    }
}
