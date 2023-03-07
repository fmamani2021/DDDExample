using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates;
using VeterinaryClinic.AppointmentModule.Shared.DTOs.AppointmentTypes;
using VeterinaryClinic.SharedKernel.Interfaces;

namespace VeterinaryClinic.AppointmentModule.Api.Controllers
{
    [ApiController]
    public class AppointmentTypeController : ControllerBase
    {
        private readonly IReadRepository<AppointmentType> _appointmentTypeRepository;
        private readonly IMapper _mapper;

        public AppointmentTypeController(
            IReadRepository<AppointmentType> appointmentTypeRepository, 
            IMapper mapper)
        {
            _appointmentTypeRepository = appointmentTypeRepository;
            _mapper = mapper;
        }

        [HttpGet(ListAppointmentTypeRequest.Route)]        
        public async Task<ActionResult<ListAppointmentTypeResponse>> List([FromQuery] ListAppointmentTypeRequest request, CancellationToken cancellationToken)
        {
            var response = new ListAppointmentTypeResponse(request.CorrelationId());

            var appointmentTypes = await _appointmentTypeRepository.ListAsync();
            response.AppointmentTypes = _mapper.Map<List<AppointmentTypeDto>>(appointmentTypes);
            response.Count = response.AppointmentTypes.Count;

            return Ok(response);
        }
    }
}
