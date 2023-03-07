using VeterinaryClinic.AppointmentModule.Shared.DTOs.Appointments;

namespace VeterinaryClinic.Presentation.ServiceAppointment
{
    public class AppointmentService
    {
        private readonly IHttpServiceAppointment _httpService;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(IHttpServiceAppointment httpService, ILogger<AppointmentService> logger)
        {
            _httpService = httpService;
            _logger = logger;
        }

        public async Task<AppointmentDto> CreateAsync(CreateAppointmentRequest appointment)
        {
            _logger.LogInformation($"Creating new appointment for {appointment.Title}");
            return (await _httpService.HttpPostAsync<CreateAppointmentResponse>(CreateAppointmentRequest.Route, appointment)).Appointment;
        }

        public async Task<AppointmentDto> EditAsync(UpdateAppointmentRequest appointment)
        {
            _logger.LogInformation($"Creating new appointment for {appointment}");
            return (await _httpService.HttpPutAsync<UpdateAppointmentResponse>(UpdateAppointmentRequest.Route, appointment)).Appointment;
        }

        public Task DeleteAsync(Guid scheduleId, Guid appointmentId)
        {
            string route = GetByIdAppointmentRequest.Route.Replace("{ScheduleId}", scheduleId.ToString());
            route = route.Replace("{AppointmentId}", appointmentId.ToString());

            return _httpService.HttpDeleteAsync<DeleteAppointmentResponse>(route);
        }

        public async Task<AppointmentDto> GetByIdAsync(Guid scheduleId, Guid appointmentId)
        {
            string route = GetByIdAppointmentRequest.Route.Replace($"{{{nameof(GetByIdAppointmentRequest.ScheduleId)}}}", scheduleId.ToString());
            route = route.Replace("{AppointmentId}", appointmentId.ToString());
            return (await _httpService.HttpGetAsync<GetByIdAppointmentResponse>(route)).Appointment;
        }

        public async Task<List<AppointmentDto>> ListAsync(Guid scheduleId)
        {
            _logger.LogInformation("Fetching appointments from API.");

            string route = ListAppointmentRequest.Route.Replace($"{{{nameof(ListAppointmentRequest.ScheduleId)}}}", scheduleId.ToString());
            return (await _httpService.HttpGetAsync<ListAppointmentResponse>(route)).Appointments;
        }
    }
}
