using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using VeterinaryClinic.ManagementModule.Domain.Aggregates;
using VeterinaryClinic.ManagementModule.Domain.ClientAgregate;
using VeterinaryClinic.ManagementModule.Domain.ValueObjects;
using VeterinaryClinic.ManagementModule.Shared.DTOs.AppointmentType;
using VeterinaryClinic.ManagementModule.Shared.DTOs.Room;

namespace VeterinaryClinic.ManagementModule.Infrastructure.Data
{
    public class AppDbContextSeed
    {
        private Doctor DrAntonio => new Doctor(1, "Dr. Antonio");
        private Doctor DrJorge => new Doctor(2, "Dr. Jorge");
        private Doctor DrYesenia => new Doctor(3, "Dr. Yesenia");
        
        private DateTime _testDate = DateTime.Now;
        public const string MALE_SEX = "Male";
        public const string FEMALE_SEX = "Female";
        private readonly AppDbContext _context;
        private readonly ILogger<AppDbContextSeed> _logger;

        public AppDbContextSeed(AppDbContext context,
          ILogger<AppDbContextSeed> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync(DateTime testDate, int? retry = 0)
        {
            _logger.LogInformation($"Seeding data.");
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
            var clientGraphs = new List<Client>();

            var clientSmith = CreateClientWithPatient("Tom Holland", "Tom", "Mr.", drAntonio.Id, MALE_SEX, "Darwin", "Dog", "Poodle");
            
            clientSmith.Patients.Add(Patient.Create(            
                clientId: 1,
                name: "Arya",
                sex: FEMALE_SEX,
                animalType: new AnimalType("Cat", "Feral"),
                preferredDoctorId: drJorge.Id                
            ));
            clientSmith.Patients.Add(Patient.Create(
                clientId: 1,
                name: "Rosie",
                sex: FEMALE_SEX,
                animalType: new AnimalType("Dog", "Golden Retriever"),
                preferredDoctorId: drJorge.Id
            ));

            clientGraphs.Add(clientSmith);

            clientGraphs.Add(CreateClientWithPatient("Scarlett Johansson", "Scarlett", "Mrs.", drGonzalo.Id, MALE_SEX, "Sampson", "Dog", "Newfoundland"));

            clientGraphs.Add(CreateClientWithPatient("Chris Evans", "Chris E.", "Mr.", drJorge.Id, MALE_SEX, "Max", "Dog", ""));

            clientGraphs.Add(CreateClientWithPatient("Chris Hemsworth", "Chris H.", "Mr.", drJorge.Id, MALE_SEX, "Violet", "Dog", "Lhasa apso"));

            clientGraphs.Add(CreateClientWithPatient("Chris Pratt", "Chris P.", "Mr.", drAntonio.Id, MALE_SEX, "Gusto", "Dog", ""));

            clientGraphs.Add(CreateClientWithPatient("Jeremy Renner", "Jeremy", "Mr.", drAntonio.Id, MALE_SEX, "Hugo", "Dog", ""));

            clientGraphs.Add(CreateClientWithPatient("Natalie Portman", "Natalie", "", drAntonio.Id, FEMALE_SEX, "Athena", "Dog", ""));

            clientGraphs.Add(CreateClientWithPatient("Brie Larson", "Brie", "", drAntonio.Id, FEMALE_SEX, "Roxy", "Dog", ""));

            clientGraphs.Add(CreateClientWithPatient("Ryan Reynolds", "Ryan", "", drAntonio.Id, MALE_SEX, "Bokeh", "Cat", ""));

            clientGraphs.Add(CreateClientWithPatient("Chadwick Boseman", "", "", drAntonio.Id, MALE_SEX, "BenFranklin", "Dog", ""));

            clientGraphs.Add(CreateClientWithPatient("Benedict Cumberbatch", "", "", drAntonio.Id, MALE_SEX, "Sugar", "Dog", ""));

            clientGraphs.Add(CreateClientWithPatient("Mark Ruffalo", "", "", drAntonio.Id, MALE_SEX, "Mim", "Cat", ""));

            clientGraphs.Add(CreateClientWithPatient("Tommy Lee Jones", "", "", drAntonio.Id, MALE_SEX, "Jasper", "Cat", ""));

            return clientGraphs;
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

            var patient = Patient.Create(
                clientId: 1,
                name: patient1Name,
                sex: patient1Sex,
                animalType: new AnimalType(animalType, breed),
                preferredDoctorId: doctorId
            );

            client.Patients.Add(patient);

            return client;
        }
    }
}
