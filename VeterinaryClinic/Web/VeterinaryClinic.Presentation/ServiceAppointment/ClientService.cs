﻿using VeterinaryClinic.AppointmentModule.Shared.DTOs.Clients;

namespace VeterinaryClinic.Presentation.ServiceAppointment
{
    public class ClientService
    {
        private readonly IHttpServiceAppointment _httpService;
        private readonly ILogger<ClientService> _logger;

        public ClientService(IHttpServiceAppointment httpService, ILogger<ClientService> logger)
        {
            _httpService = httpService;
            _logger = logger;
        }

        public async Task<ClientDto> CreateAsync(CreateClientRequest client)
        {
            return (await _httpService.HttpPostAsync<CreateClientResponse>("clients", client)).Client;
        }

        public async Task<ClientDto> EditAsync(UpdateClientRequest client)
        {
            return (await _httpService.HttpPutAsync<UpdateClientResponse>("clients", client)).Client;
        }

        public Task DeleteAsync(int clientId)
        {
            return _httpService.HttpDeleteAsync<DeleteClientResponse>("clients", clientId);
        }

        public async Task<ClientDto> GetByIdAsync(int clientId)
        {
            return (await _httpService.HttpGetAsync<GetByIdClientResponse>($"clients/{clientId}")).Client;
        }

        public async Task<List<ClientDto>> ListPagedAsync(int pageSize)
        {
            _logger.LogInformation("Fetching clients from API.");

            return (await _httpService.HttpGetAsync<ListClientResponse>(ListClientRequest.Route)).Clients;
        }

        public async Task<List<ClientDto>> ListAsync()
        {
            _logger.LogInformation("Fetching clients from API.");

            return (await _httpService.HttpGetAsync<ListClientResponse>(ListClientRequest.Route)).Clients;
        }
    }
}
