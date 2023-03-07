using VeterinaryClinic.AppointmentModule.Shared.DTOs.AppointmentTypes;

namespace VeterinaryClinic.Presentation.ServiceAppointment
{
    public class AppointmentTypeService
    {
        private readonly IHttpServiceAppointment _httpService;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentTypeService(IHttpServiceAppointment httpService, ILogger<AppointmentService> logger)
        {
            _httpService = httpService;
            _logger = logger;
        }

        public async Task<List<AppointmentTypeDto>> ListPagedAsync(int pageSize)
        {
            _logger.LogInformation("Fetching appointment types from API.");

            return (await _httpService.HttpGetAsync<ListAppointmentTypeResponse>(ListAppointmentTypeRequest.Route)).AppointmentTypes;
        }

        public async Task<List<AppointmentTypeDto>> ListAsync()
        {
            _logger.LogInformation("Fetching appointment types from API.");

            return (await _httpService.HttpGetAsync<ListAppointmentTypeResponse>(ListAppointmentTypeRequest.Route)).AppointmentTypes;
        }
    }
}
