using VeterinaryClinic.AppointmentModule.Shared.DTOs.Doctors;

namespace VeterinaryClinic.Presentation.ServiceAppointment
{
    public class DoctorService
    {
        private readonly IHttpServiceAppointment _httpService;
        private readonly ILogger<DoctorService> _logger;

        public DoctorService(IHttpServiceAppointment httpService, ILogger<DoctorService> logger)
        {
            _httpService = httpService;
            _logger = logger;
        }

        public async Task<DoctorDto> CreateAsync(CreateDoctorRequest doctor)
        {
            return (await _httpService.HttpPostAsync<CreateDoctorResponse>("doctors", doctor)).Doctor;
        }

        public async Task<DoctorDto> EditAsync(UpdateDoctorRequest doctor)
        {
            return (await _httpService.HttpPutAsync<UpdateDoctorResponse>("doctors", doctor)).Doctor;
        }

        public Task DeleteAsync(int doctorId)
        {
            return _httpService.HttpDeleteAsync<DeleteDoctorResponse>("doctors", doctorId);
        }

        public async Task<DoctorDto> GetByIdAsync(int doctorId)
        {
            return (await _httpService.HttpGetAsync<GetByIdDoctorResponse>($"doctors/{doctorId}")).Doctor;
        }

        public async Task<List<DoctorDto>> ListPagedAsync(int pageSize)
        {
            _logger.LogInformation("Fetching doctors from API.");

            return (await _httpService.HttpGetAsync<ListDoctorResponse>(ListDoctorRequest.Route)).Doctors;
        }

        public async Task<List<DoctorDto>> ListAsync()
        {
            _logger.LogInformation("Fetching doctors from API.");

            return (await _httpService.HttpGetAsync<ListDoctorResponse>(ListDoctorRequest.Route)).Doctors;
        }
    }
}
