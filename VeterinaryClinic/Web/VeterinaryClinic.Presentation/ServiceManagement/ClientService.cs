using VeterinaryClinic.ManagementModule.Shared.DTOs.Client;

namespace VeterinaryClinic.Presentation.ServiceManagement
{
    public class ClientService
    {
        private readonly IHttpServiceManagement _httpService;
        private readonly ILogger<ClientService> _logger;

        public ClientService(IHttpServiceManagement httpService, ILogger<ClientService> logger)
        {
            _httpService = httpService;
            _logger = logger;
        }
        public async Task<List<ClientDto>> ListAsync()
        {
            return (await _httpService.HttpGetAsync<ListClientResponse>(ListClientRequest.Route)).Clients;
        }

        public async Task<ClientDto> GetByIdAsync(int clientId)
        {
            return (await _httpService.HttpGetAsync<GetByIdClientResponse>(GetByIdClientRequest.Route)).Client;
        }

        public async Task<ClientDto> CreateAsync(CreateClientRequest client)
        {
            return (await _httpService.HttpPostAsync<CreateClientResponse>(CreateClientRequest.Route, client)).Client;
        }

        public async Task<ClientDto> EditAsync(UpdateClientRequest client)
        {
            return (await _httpService.HttpPutAsync<UpdateClientResponse>(UpdateClientRequest.Route, client)).Client;
        }

        public async Task DeleteAsync(int clientId)
        {
            string route = DeleteClientRequest.Route.Replace("{id}", clientId.ToString());
            await _httpService.HttpDeleteAsync<DeleteClientResponse>(route);
        }
    }
}
