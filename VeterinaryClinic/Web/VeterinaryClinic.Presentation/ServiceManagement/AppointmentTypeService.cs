using VeterinaryClinic.ManagementModule.Shared.DTOs.AppointmentType;

namespace VeterinaryClinic.Presentation.ServiceManagement
{
    public class AppointmentTypeService
    {
        private readonly IHttpServiceManagement _httpService;
        private readonly ILogger<AppointmentTypeService> _logger;

        public AppointmentTypeService(IHttpServiceManagement httpService, ILogger<AppointmentTypeService> logger)
        {
            _httpService = httpService;
            _logger = logger;
        }

        public async Task<List<AppointmentTypeDto>> ListPagedAsync(int pageSize)
        {
            _logger.LogInformation("Fetching appointment types from API.");

            return (await _httpService.HttpGetAsync<ListAppointmentTypeResponse>($"appointment-types")).AppointmentTypes;
        }

        public async Task<List<AppointmentTypeDto>> ListAsync()
        {
            _logger.LogInformation("Fetching appointment types from API.");

            return (await _httpService.HttpGetAsync<ListAppointmentTypeResponse>($"appointment-types")).AppointmentTypes;
        }
    }
}
