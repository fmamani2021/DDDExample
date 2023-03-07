using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate;
using VeterinaryClinic.AppointmentModule.Shared.DTOs.Schedules;
using VeterinaryClinic.SharedKernel.Interfaces;

namespace VeterinaryClinic.AppointmentModule.Api.Controllers
{
    [ApiController]
    public class SchedulerController : ControllerBase
    {
        private readonly IReadRepository<Schedule> _repository;
        private readonly IMapper _mapper;

        public SchedulerController(IReadRepository<Schedule> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet(ListScheduleRequest.Route)]
        public async Task<ActionResult<ListScheduleResponse>> List(
            [FromQuery] ListScheduleRequest request, 
            CancellationToken cancellationToken)
        {
            var response = new ListScheduleResponse(request.CorrelationId());
            var schedules = await _repository.ListAsync();
            if (schedules is null) return NotFound();

            response.Schedules = _mapper.Map<List<ScheduleDto>>(schedules);
            response.Count = response.Schedules.Count;
            return Ok(response);
        }
    }
}
