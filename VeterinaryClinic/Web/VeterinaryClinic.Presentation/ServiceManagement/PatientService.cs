﻿using VeterinaryClinic.ManagementModule.Shared.DTOs.Patient;

namespace VeterinaryClinic.Presentation.ServiceManagement
{
    public class PatientService
    {
        private readonly IHttpServiceManagement _httpService;
        private readonly ILogger<PatientService> _logger;

        public PatientService(IHttpServiceManagement httpService, ILogger<PatientService> logger)
        {
            _httpService = httpService;
            _logger = logger;
        }

        public async Task<PatientDto> CreateAsync(CreatePatientRequest patient)
        {
            return (await _httpService.HttpPostAsync<CreatePatientResponse>("patients", patient)).Patient;
        }

        public async Task<PatientDto> EditAsync(UpdatePatientRequest patient)
        {
            return (await _httpService.HttpPutAsync<UpdatePatientResponse>("patients", patient)).Patient;
        }

        public Task DeleteAsync(int patientId)
        {
            return _httpService.HttpDeleteAsync<DeletePatientResponse>("patients", patientId);
        }

        public async Task<PatientDto> GetByIdAsync(int patientId)
        {
            return (await _httpService.HttpGetAsync<GetByIdPatientResponse>($"patients/{patientId}")).Patient;
        }

        public async Task<List<PatientDto>> ListPagedAsync(int pageSize)
        {
            _logger.LogInformation("Fetching patients from API.");

            return (await _httpService.HttpGetAsync<ListPatientResponse>($"patients")).Patients;
        }

        public async Task<List<PatientDto>> ListAsync(int clientId)
        {
            _logger.LogInformation("Fetching patients from API.");

            return (await _httpService.HttpGetAsync<ListPatientResponse>($"patients?ClientId={clientId}")).Patients;
        }
    }
}
