using VeterinaryClinic.ManagementModule.Shared.DTOs.Doctor;

namespace VeterinaryClinic.Presentation.ServiceManagement
{
    public class DoctorService
    {
        private readonly IHttpServiceManagement _httpService;
        private readonly ILogger<DoctorService> _logger;

        public DoctorService(IHttpServiceManagement httpService, ILogger<DoctorService> logger)
        {
            _httpService = httpService;
            _logger = logger;
        }

        public async Task<List<DoctorDto>> ListAsync()
        {
            _logger.LogInformation("Fetching doctors from API.");

            return (await _httpService.HttpGetAsync<ListDoctorResponse>(ListDoctorRequest.Route)).Doctors;
        }
    }
}
