﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate;
using VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates;
using VeterinaryClinic.AppointmentModule.Domain.ValueObjects;
using VeterinaryClinic.AppointmentModule.Shared.DTOs.AppointmentTypes;
using VeterinaryClinic.AppointmentModule.Shared.DTOs.Rooms;
using VeterinaryClinic.SharedKernel.ValueObjects.Custom;

namespace VeterinaryClinic.AppointmentModule.Infrastructure.Data
{
    public class AppDbContextSeed
    {
        private Doctor DrAntonio => new Doctor(1, "Dr. Antonio");
        private Doctor DrJorge => new Doctor(2, "Dr. Jorge");
        private Doctor DrYesenia => new Doctor(3, "Dr. Yesenia");

        private readonly Guid _scheduleId = Guid.Parse("f9369039-9d11-4442-9738-ed65d8a8ad52");
        private readonly int _clinicId = 1;
        private DateTimeOffset _testDate = DateTime.UtcNow.Date;
        public const string MALE_SEX = "Male";
        public const string FEMALE_SEX = "Female";
        private readonly AppDbContext _context;
        private readonly ILogger<AppDbContextSeed> _logger;
        private Client _tom;
        private Client _scarlett;
        private Patient _darwin;
        private Patient _sampson;

        public AppDbContextSeed(AppDbContext context,
          ILogger<AppDbContextSeed> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync(DateTimeOffset testDate, int? retry = 0)
        {
            _logger.LogInformation($"Seeding data - testDate: {testDate}");
            _logger.LogInformation($"DbContext Type: {_context.Database.ProviderName}");

            _testDate = testDate;
            int retryForAvailability = retry.Value;
            try
            {
                if (_context.IsRealDatabase())
                {
                    // apply migrations if connecting to a SQL database
                    _context.Database.Migrate();
                }

                if (!await _context.Schedules.AnyAsync())
                {
                    await _context.Schedules.AddAsync(
                        CreateSchedule());

                    await _context.SaveChangesAsync();
                }

                if (!await _context.AppointmentTypes.AnyAsync())
                {
                    var apptTypes = await CreateAppointmentTypes();
                    await _context.AppointmentTypes.AddRangeAsync(apptTypes);
                    await _context.SaveChangesWithIdentityInsert<AppointmentType>();
                }

                if (!await _context.Doctors.AnyAsync())
                {
                    var doctors = CreateDoctors();
                    await _context.Doctors.AddRangeAsync(doctors);
                    await _context.SaveChangesWithIdentityInsert<Doctor>();
                }

                if (!await _context.Clients.AnyAsync())
                {
                    await _context.Clients.AddRangeAsync(
                        CreateListOfClientsWithPatients(DrAntonio, DrJorge, DrYesenia));

                    await _context.SaveChangesAsync();
                }

                if (!await _context.Rooms.AnyAsync())
                {
                    var rooms = await CreateRooms();
                    await _context.Rooms.AddRangeAsync(rooms);
                    await _context.SaveChangesWithIdentityInsert<Room>();
                }

                if (!await _context.Appointments.AnyAsync())
                {
                    _tom = _context.Clients.FirstOrDefault(c => c.FullName == "Tom Holland");
                    _scarlett = _context.Clients.FirstOrDefault(c => c.FullName == "Scarlett Johansson");
                    _darwin = _context.Patients.FirstOrDefault(p => p.Name == "Darwin");
                    _sampson = _context.Patients.FirstOrDefault(p => p.Name == "Sampson");
                    await _context.Appointments.AddRangeAsync(CreateAppointments(_scheduleId));

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 1)
                {
                    retryForAvailability++;
                    _logger.LogError(ex.Message);
                    await SeedAsync(_testDate, retryForAvailability);
                }
                throw;
            }

            await _context.SaveChangesAsync();
        }

        private async Task<List<Room>> CreateRooms()
        {
            string fileName = "rooms.json";
            if (!File.Exists(fileName))
            {
                _logger.LogInformation($"Creating {fileName}");
                using Stream writer = new FileStream(fileName, FileMode.OpenOrCreate);
                await JsonSerializer.SerializeAsync(writer, GetDefaultRooms());
            }

            _logger.LogInformation($"Reading rooms from file {fileName}");
            using Stream reader = new FileStream(fileName, FileMode.Open);
            var rooms = await JsonSerializer.DeserializeAsync<List<RoomDto>>(reader);

            return rooms.Select(dto => new Room(dto.RoomId, dto.Name)).ToList();
        }

        private List<RoomDto> GetDefaultRooms()
        {
            List<RoomDto> rooms = new List<RoomDto>();
            for (int i = 1; i < 6; i++)
            {
                rooms.Add(new RoomDto { RoomId = i, Name = $"Exam Room {i}" });
            }
            return rooms;
        }

        private Schedule CreateSchedule()
        {
            return new Schedule(_scheduleId, DateTimeOffsetRange.Create(_testDate, _testDate), _clinicId);
        }

        private async Task<List<AppointmentType>> CreateAppointmentTypes()
        {
            string fileName = "appointmentTypes.json";
            if (!File.Exists(fileName))
            {
                _logger.LogInformation($"Creating {fileName}");
                using Stream writer = new FileStream(fileName, FileMode.OpenOrCreate);
                await JsonSerializer.SerializeAsync(writer, GetDefaultAppointmentTypes());
            }

            _logger.LogInformation($"Reading appointment types from file {fileName}");
            using Stream reader = new FileStream(fileName, FileMode.Open);
            var apptTypes = await JsonSerializer.DeserializeAsync<List<AppointmentTypeDto>>(reader);

            return apptTypes.Select(dto => new AppointmentType(dto.AppointmentTypeId, dto.Name, dto.Code, dto.Duration)).ToList();
        }

        private List<AppointmentTypeDto> GetDefaultAppointmentTypes()
        {
            var result = new List<AppointmentTypeDto>
            {
                new AppointmentTypeDto {
                  AppointmentTypeId=1,
                  Name="Wellness Exam",
                  Code="WE",
                  Duration=30
                },
                new AppointmentTypeDto {
                  AppointmentTypeId=2,
                  Name="Diagnostic Exam",
                  Code="DE",
                  Duration=60
                },
                new AppointmentTypeDto{
                  AppointmentTypeId=3,
                  Name="Nail Trim",
                  Code="NT",
                  Duration=30
                }
            };

            return result;
        }

        private List<Doctor> CreateDoctors()
        {
            var result = new List<Doctor>
            {
                DrAntonio,
                DrJorge,
                DrYesenia
            };

            return result;
        }

        private IEnumerable<Client> CreateListOfClientsWithPatients(Doctor drAntonio, Doctor drJorge, Doctor drGonzalo)
        {
            var clients = new List<Client>();

            var clientTom = CreateClientWithPatient("Tom Holland", "Tom", "Mr.", drAntonio.Id, MALE_SEX, "Darwin", "Dog", "Poodle");
            clientTom.Patients.Add(new Patient(1, "Arya", FEMALE_SEX, new AnimalType("Cat", "Feral"), drJorge.Id));
            clientTom.Patients.Add(new Patient(1, "Rosie", FEMALE_SEX, new AnimalType("Dog", "Golden Retriever"), drJorge.Id));

            clients.Add(clientTom);

            clients.Add(CreateClientWithPatient("Scarlett Johansson", "Scarlett", "Mrs.", drGonzalo.Id, MALE_SEX, "Sampson", "Dog", "Newfoundland"));

            clients.Add(CreateClientWithPatient("Chris Evans", "Chris E.", "Mr.", drJorge.Id, MALE_SEX, "Max", "Dog", ""));

            clients.Add(CreateClientWithPatient("Chris Hemsworth", "Chris H.", "Mr.", drJorge.Id, MALE_SEX, "Violet", "Dog", "Lhasa apso"));

            clients.Add(CreateClientWithPatient("Chris Pratt", "Chris P.", "Mr.", drAntonio.Id, MALE_SEX, "Gusto", "Dog", ""));

            clients.Add(CreateClientWithPatient("Jeremy Renner", "Jeremy", "Mr.", drAntonio.Id, MALE_SEX, "Hugo", "Dog", ""));

            clients.Add(CreateClientWithPatient("Natalie Portman", "Natalie", "", drAntonio.Id, FEMALE_SEX, "Athena", "Dog", ""));

            clients.Add(CreateClientWithPatient("Brie Larson", "Brie", "", drAntonio.Id, FEMALE_SEX, "Roxy", "Dog", ""));

            clients.Add(CreateClientWithPatient("Ryan Reynolds", "Ryan", "", drAntonio.Id, MALE_SEX, "Bokeh", "Cat", ""));

            clients.Add(CreateClientWithPatient("Chadwick Boseman", "", "", drAntonio.Id, MALE_SEX, "BenFranklin", "Dog", ""));

            clients.Add(CreateClientWithPatient("Benedict Cumberbatch", "", "", drAntonio.Id, MALE_SEX, "Sugar", "Dog", ""));

            clients.Add(CreateClientWithPatient("Mark Ruffalo", "", "", drAntonio.Id, MALE_SEX, "Mim", "Cat", ""));

            clients.Add(CreateClientWithPatient("Tommy Lee Jones", "", "", drAntonio.Id, MALE_SEX, "Jasper", "Cat", ""));

            return clients;
        }

        private static Client CreateClientWithPatient(string fullName,
            string preferredName,
            string salutation,
            int doctorId,
            string patient1Sex,
            string patient1Name,
            string animalType,
            string breed)
        {
            var client = new Client(fullName, preferredName, salutation, doctorId, "client@example.com");
            client.Patients.Add(new Patient(client.Id, patient1Name, patient1Sex, new AnimalType(animalType, breed), doctorId));

            return client;
        }

        private IEnumerable<Appointment> CreateAppointments(Guid scheduleId)
        {
            int wellnessVisit = 1;
            int diagnosticVisit = 2;
            int room1 = 1;
            int room2 = 2;
            int room3 = 3;
            int room4 = 4;
            var appointmentList = new List<Appointment>();

            appointmentList.Add(new Appointment(
                    Guid.NewGuid(),
                    wellnessVisit,
                    scheduleId,
                    _tom.Id,
                    DrAntonio.Id,
                    _darwin.Id,
                    room1,
                    DateTimeOffsetRange.CreateWithDuration(_testDate.AddHours(10), TimeSpan.FromMinutes(30)),
                    "(WE) Darwin - Tom Holland"));

            var appty = new Appointment(
                    Guid.NewGuid(),
                    wellnessVisit,
                    scheduleId,
                    _tom.Id,
                    DrAntonio.Id,
                    _tom.Patients[1].Id,
                    room1,
                    DateTimeOffsetRange.CreateWithDuration(_testDate.AddHours(10).AddMinutes(30), TimeSpan.FromMinutes(30)),
                    "(WE) Arya - Tom Holland");

            appointmentList.Add(appty);

            appointmentList.Add(new Appointment(
                    Guid.NewGuid(),
                    wellnessVisit,
                    scheduleId,
                    _tom.Id,
                    DrAntonio.Id,
                    _tom.Patients[2].Id,
                    room1,
                    DateTimeOffsetRange.CreateWithDuration(_testDate.AddHours(11), TimeSpan.FromMinutes(30)),
                    "(WE) Rosie - Tom Holland"));

            appointmentList.Add(new Appointment(
                    Guid.NewGuid(),
                    diagnosticVisit,
                    scheduleId,
                    _scarlett.Id,
                    DrJorge.Id,
                    _sampson.Id,
                    room2,
                    DateTimeOffsetRange.CreateWithDuration(_testDate.AddHours(11), TimeSpan.FromMinutes(60)),
                    "(DE) Sampson - Scarlett Johansson"));

            appointmentList.Add(CreateAppointment(scheduleId, "Chris Evans", "Max", room3, 10));
            appointmentList.Add(CreateAppointment(scheduleId, "Chris Pratt", "Gusto", room3, 11));
            appointmentList.Add(CreateAppointment(scheduleId, "Jeremy Renner", "Hugo", room3, 13));
            appointmentList.Add(CreateAppointment(scheduleId, "Natalie Portman", "Athena", room3, 14));
            appointmentList.Add(CreateAppointment(scheduleId, "Brie Larson", "Roxy", room3, 15));
            appointmentList.Add(CreateAppointment(scheduleId, "Ryan Reynolds", "Bokeh", room3, 16));
            appointmentList.Add(CreateAppointment(scheduleId, "Chadwick Boseman", "BenFranklin", room4, 8));
            appointmentList.Add(CreateAppointment(scheduleId, "Benedict Cumberbatch", "Sugar", room4, 9));
            appointmentList.Add(CreateAppointment(scheduleId, "Mark Ruffalo", "Mim", room4, 10));
            appointmentList.Add(CreateAppointment(scheduleId, "Tommy Lee Jones", "Jasper", room4, 11));
            appointmentList.Add(CreateAppointment(scheduleId, "Chris Hemsworth", "Violet", room4, 13));

            return appointmentList;
        }

        private Appointment CreateAppointment(Guid scheduleId, string clientName, string patientName, int roomId, int hour)
        {
            int diagnosticVisit = 2;
            try
            {
                var client = _context.Clients.First(c => c.FullName == clientName);
                var patient = _context.Patients.First(p => p.Name == patientName);
                var appt = new Appointment(Guid.NewGuid(),
                  diagnosticVisit,
                  scheduleId,
                  client.Id,
                  patient.PreferredDoctorId.Value,
                  patient.Id, roomId,
                  DateTimeOffsetRange.CreateWithDuration(_testDate.AddHours(hour), TimeSpan.FromMinutes(60)),
                  $"(DE) {patientName} - {clientName}");
                return appt;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating appointment for {clientName}, {patientName}", ex);
            }
        }
    }
}
