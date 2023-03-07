namespace VeterinaryClinic.Presentation.ServiceAppointment
{
    public class ConfigurationService
    {
        private readonly IHttpServiceAppointment _httpService;
        private readonly ILogger<ConfigurationService> _logger;

        public ConfigurationService(IHttpServiceAppointment httpService, ILogger<ConfigurationService> logger)
        {
            _httpService = httpService;
            _logger = logger;
        }

        public async Task<DateTime> ReadAsync()
        {
            _logger.LogInformation("Read today date/time from configuration.");

            return Convert.ToDateTime(await _httpService.HttpGetAsync($"api/configurations"));
        }
    }
}
