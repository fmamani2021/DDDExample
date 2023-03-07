using AutoFixture;
using AutoFixture.Kernel;
using VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate;
using VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate.Events;
using VeterinaryClinic.SharedKernel.ValueObjects.Custom;
using Xunit;

namespace VeterinaryClinic.AppointmentModule.UnitTesting.Core.ScheduleAggregate.AppointmentTests
{
    public class Appointment_UpdateRoom
    {
        private readonly Fixture _fixture = new Fixture();
        public Appointment_UpdateRoom()
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
        public void DoesNothingGivenSameRoomId()
        {
            var appointment = GetUnconfirmedAppointment();
            var initialRoomId = appointment.RoomId;
            var initialEventCount = appointment.DomainEvents.Count;

            appointment.UpdateRoom(initialRoomId);

            Assert.Equal(initialRoomId, appointment.RoomId);
            Assert.Equal(initialEventCount, appointment.DomainEvents.Count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ThrowsGivenNegativeOrZeroRoomId(int invalidRoomId)
        {
            var appointment = GetUnconfirmedAppointment();

            Action action = () => appointment.UpdateRoom(invalidRoomId);

            Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void UpdatesRoomIdAndAddsEventGivenNewRoomId()
        {
            var appointment = GetUnconfirmedAppointment();
            var initialRoomId = appointment.RoomId;
            var initialEventCount = appointment.DomainEvents.Count;

            int newRoomId = initialRoomId + 1;
            appointment.UpdateRoom(newRoomId);

            Assert.Equal(newRoomId, appointment.RoomId);
            Assert.Single(appointment.DomainEvents);
            Assert.Contains(appointment.DomainEvents, x => x.GetType() == typeof(AppointmentUpdatedEvent));
        }
    }
}
