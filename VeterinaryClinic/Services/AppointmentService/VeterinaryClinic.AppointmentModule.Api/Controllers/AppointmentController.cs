using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.AppointmentModule.Domain.Exceptions;
using VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate;
using VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate.Specifications;
using VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates;
using VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates.Specifications;
using VeterinaryClinic.AppointmentModule.Shared.DTOs.Appointments;
using VeterinaryClinic.AppointmentModule.Shared.DTOs.Schedules;
using VeterinaryClinic.SharedKernel.Interfaces;
using VeterinaryClinic.SharedKernel.ValueObjects.Custom;

namespace VeterinaryClinic.AppointmentModule.Api.Controllers
{
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IRepository<Schedule> _scheduleRepository;
        private readonly IReadRepository<AppointmentType> _appointmentTypeReadRepository;
        private readonly IReadRepository<Schedule> _scheduleReadRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AppointmentController> _logger;
        private readonly IReadRepository<Client> _clientRepository;

        public AppointmentController(
            IRepository<Schedule> scheduleRepository,
            IReadRepository<Schedule> scheduleReadRepository,
            IReadRepository<AppointmentType> appointmentTypeReadRepository,
            IReadRepository<Client> clientRepository,
            IMapper mapper,
            ILogger<AppointmentController> logger)
        {
            _scheduleRepository = scheduleRepository;
            _scheduleReadRepository = scheduleReadRepository;
            _appointmentTypeReadRepository = appointmentTypeReadRepository;
            _clientRepository = clientRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost(CreateAppointmentRequest.Route)]
        public async Task<IActionResult> Create(
           CreateAppointmentRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateAppointmentResponse(request.CorrelationId());

            var spec = new ScheduleByIdWithAppointmentsSpec(request.ScheduleId);
            Schedule schedule = await _scheduleRepository.FirstOrDefaultAsync(spec, cancellationToken);
            
            var appointmentType = await _appointmentTypeReadRepository.GetByIdAsync(request.AppointmentTypeId);
            var appointmentStart = request.DateOfAppointment;
            
            var timeRange = DateTimeOffsetRange.CreateWithDuration(appointmentStart, TimeSpan.FromMinutes(appointmentType.Duration));

            //var timeRange2 = DateTimeOffsetRange.CreateWithDuration(appointmentStart, TimeSpan.FromMinutes(appointmentType.Duration));
            //if (timeRange == timeRange2)
            //{
            //    //true
            //}

            var newAppointment = Appointment.Create(Guid.NewGuid(), request.AppointmentTypeId, request.ScheduleId,
              request.ClientId, request.SelectedDoctor, request.PatientId, request.RoomId, timeRange, request.Title);

            schedule.AddNewAppointment(newAppointment);

            await _scheduleRepository.UpdateAsync(schedule);
            
            var dto = _mapper.Map<AppointmentDto>(newAppointment);
            
            response.Appointment = dto;

            return Ok(response);
        }

        [HttpGet(ListAppointmentRequest.Route)]
        public async Task<IActionResult> Get(
            [FromRoute] ListAppointmentRequest request, CancellationToken cancellationToken)
        {
            var response = new ListAppointmentResponse(request.CorrelationId());
            Schedule schedule = null;
            if (request.ScheduleId == Guid.Empty)
            {
                return NotFound();
            }

            // TODO: Get date from API request and use a specification that only includes appointments on that date.
            var spec = new ScheduleByIdWithAppointmentsSpec(request.ScheduleId);
            schedule = await _scheduleRepository.FirstOrDefaultAsync(spec, cancellationToken);
            if (schedule == null) throw new ScheduleNotFoundException($"No schedule found for id {request.ScheduleId}.");

            int conflictedAppointmentsCount = schedule.Appointments
              .Count(a => a.IsPotentiallyConflicting);
            _logger.LogInformation($"API:ListAppointments There are now {conflictedAppointmentsCount} conflicted appointments.");

            var myAppointments = _mapper.Map<List<AppointmentDto>>(schedule.Appointments);

            // load names - only do this kind of thing if you have caching!
            // N+1 query problem
            // Possibly use custom SQL or view or stored procedure instead
            foreach (var appt in myAppointments)
            {
                var clientSpec = new ClientByIdIncludePatientsSpecification(appt.ClientId);
                var client = await _clientRepository.FirstOrDefaultAsync(clientSpec, cancellationToken);
                var patient = client.Patients.First(p => p.Id == appt.PatientId);

                appt.ClientName = client.FullName;
                appt.PatientName = patient.Name;
            }

            response.Appointments = myAppointments.OrderBy(a => a.Start).ToList();
            response.Count = response.Appointments.Count;

            return Ok(response);
        }

        [HttpGet(GetByIdAppointmentRequest.Route)]
        public async Task<IActionResult> GetById(
            [FromRoute] GetByIdAppointmentRequest request, CancellationToken cancellationToken)
        {
            var response = new GetByIdAppointmentResponse(request.CorrelationId());

            var spec = new ScheduleByIdWithAppointmentsSpec(request.ScheduleId); // TODO: Just get that day's appointments
            var schedule = await _scheduleRepository.FirstOrDefaultAsync(spec, cancellationToken);

            var appointment = schedule.Appointments.FirstOrDefault(a => a.Id == request.AppointmentId);
            if (appointment == null) return NotFound();

            response.Appointment = _mapper.Map<AppointmentDto>(appointment);

            // load names
            var clientSpec = new ClientByIdIncludePatientsSpecification(appointment.ClientId);
            var client = await _clientRepository.FirstOrDefaultAsync(clientSpec, cancellationToken); ;
            var patient = client.Patients.First(p => p.Id == appointment.PatientId);

            response.Appointment.ClientName = client.FullName;
            response.Appointment.PatientName = patient.Name;

            return Ok(response);
        }
        
        [HttpPut(UpdateAppointmentRequest.Route)]
        public async Task<IActionResult> Update(
            UpdateAppointmentRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateAppointmentResponse(request.CorrelationId());

            var apptType = await _appointmentTypeReadRepository.GetByIdAsync(request.AppointmentTypeId);

            var spec = new ScheduleByIdWithAppointmentsSpec(request.ScheduleId); // TODO: Just get that day's appointments
            var schedule = await _scheduleReadRepository.FirstOrDefaultAsync(spec, cancellationToken);

            var apptToUpdate = schedule.Appointments.FirstOrDefault(a => a.Id == request.Id);
            apptToUpdate.UpdateAppointmentType(apptType, schedule.AppointmentUpdatedHandler);
            apptToUpdate.UpdateRoom(request.RoomId);
            apptToUpdate.UpdateStartTime(request.Start, schedule.AppointmentUpdatedHandler);
            apptToUpdate.UpdateTitle(request.Title);
            apptToUpdate.UpdateDoctor(request.DoctorId);

            await _scheduleRepository.UpdateAsync(schedule);

            var dto = _mapper.Map<AppointmentDto>(apptToUpdate);
            response.Appointment = dto;

            return Ok(response);
        }

        [HttpDelete(DeleteAppointmentRequest.Route)]
        public async Task<IActionResult> Delete(
            [FromRoute] DeleteAppointmentRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteAppointmentResponse(request.CorrelationId());

            var spec = new ScheduleByIdWithAppointmentsSpec(request.ScheduleId); // TODO: Just get that day's appointments
            var schedule = await _scheduleRepository.FirstOrDefaultAsync(spec, cancellationToken);

            var apptToDelete = schedule.Appointments.FirstOrDefault(a => a.Id == request.AppointmentId);
            if (apptToDelete == null) return NotFound();

            schedule.DeleteAppointment(apptToDelete);

            await _scheduleRepository.UpdateAsync(schedule);

            // verify we can still get the schedule
            response.Schedule = _mapper.Map<ScheduleDto>(await _scheduleRepository.FirstOrDefaultAsync(spec, cancellationToken));

            return Ok(response);
        }

    }
}
