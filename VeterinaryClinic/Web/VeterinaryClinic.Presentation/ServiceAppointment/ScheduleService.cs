using VeterinaryClinic.AppointmentModule.Shared.DTOs.Schedules;

namespace VeterinaryClinic.Presentation.ServiceAppointment
{
    public class ScheduleService
    {
        private readonly IHttpServiceAppointment _httpService;
        private readonly ILogger<ScheduleService> _logger;

        public ScheduleService(IHttpServiceAppointment httpService,
          ILogger<ScheduleService> logger)
        {
            _httpService = httpService;
            _logger = logger;
        }

        public async Task<List<ScheduleDto>> ListAsync()
        {
            _logger.LogInformation("Fetching schedules from API.");

            var route = ListScheduleRequest.Route;
            return (await _httpService.HttpGetAsync<ListScheduleResponse>(route)).Schedules;
        }
    }
}
