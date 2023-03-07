using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates;
using VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates.Specifications;
using VeterinaryClinic.AppointmentModule.Shared.DTOs.Clients;
using VeterinaryClinic.SharedKernel.Interfaces;

namespace VeterinaryClinic.AppointmentModule.Api.Controllers
{
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IReadRepository<Client> _repository;
        private readonly IMapper _mapper;

        public ClientController(
            IReadRepository<Client> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet(ListClientRequest.Route)]        
        public async Task<ActionResult<ListClientResponse>> List(
            [FromQuery] ListClientRequest request,
            CancellationToken cancellationToken)
        {
            var response = new ListClientResponse(request.CorrelationId());

            var spec = new ClientsIncludePatientsSpecification();
            var clients = await _repository.ListAsync(spec);
            if (clients is null) return NotFound();

            response.Clients = _mapper.Map<List<ClientDto>>(clients);
            response.Count = response.Clients.Count;

            return Ok(response);
        }

        [HttpGet(GetByIdClientRequest.Route)]       
        public async Task<ActionResult<GetByIdClientResponse>> GetById(
            [FromRoute] GetByIdClientRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdClientResponse(request.CorrelationId());

            // TODO: Use specification and consider including patients
            var client = await _repository.GetByIdAsync(request.ClientId);
            if (client is null) return NotFound();

            response.Client = _mapper.Map<ClientDto>(client);

            return Ok(response);
        }
    }
}
