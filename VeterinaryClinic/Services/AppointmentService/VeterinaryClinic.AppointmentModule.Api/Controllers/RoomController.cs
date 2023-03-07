using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates;
using VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates.Specifications;
using VeterinaryClinic.AppointmentModule.Shared.DTOs.Rooms;
using VeterinaryClinic.SharedKernel.Interfaces;

namespace VeterinaryClinic.AppointmentModule.Api.Controllers
{
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IReadRepository<Room> _repository;
        private readonly IMapper _mapper;

        public RoomController(IReadRepository<Room> repository,
          IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet(ListRoomRequest.Route)]        
        public async Task<ActionResult<ListRoomResponse>> List(
            [FromQuery] ListRoomRequest request,
            CancellationToken cancellationToken)
        {
            var response = new ListRoomResponse(request.CorrelationId());

            var roomSpec = new RoomSpecification();
            var rooms = await _repository.ListAsync(roomSpec);
            if (rooms is null) return NotFound();

            response.Rooms = _mapper.Map<List<RoomDto>>(rooms);
            response.Count = response.Rooms.Count;

            return Ok(response);
        }
    }
}
