namespace VeterinaryClinic.Presentation.ServiceManagement
{
    public class ConfigurationService
    {
        private readonly IHttpServiceManagement _httpService;
        private readonly ILogger<ConfigurationService> _logger;

        public ConfigurationService(IHttpServiceManagement httpService, ILogger<ConfigurationService> logger)
        {
            _httpService = httpService;
            _logger = logger;
        }

        public async Task<DateTime> ReadAsync()
        {
            _logger.LogInformation("Read today date/time from configuration.");

            return Convert.ToDateTime(await _httpService.HttpGetAsync($"configurations"));
        }
    }
}
