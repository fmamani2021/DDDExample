using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates;
using VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates.Specifications;
using VeterinaryClinic.AppointmentModule.Shared.DTOs.Patients;
using VeterinaryClinic.SharedKernel.Interfaces;

namespace VeterinaryClinic.AppointmentModule.Api.Controllers
{
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IReadRepository<Client> _repository;
        private readonly IMapper _mapper;

        public PatientController(IReadRepository<Client> repository,
          IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet(ListPatientRequest.Route)]        
        public async Task<ActionResult<ListPatientResponse>> List(
            [FromRoute] ListPatientRequest request,
            CancellationToken cancellationToken)
        {
            var response = new ListPatientResponse(request.CorrelationId());

            var spec = new ClientByIdIncludePatientsSpecification(request.ClientId);
            var client = await _repository.GetBySpecAsync(spec);
            if (client == null) return NotFound();

            response.Patients = _mapper.Map<List<PatientDto>>(client.Patients);
            response.Patients.ForEach(p =>
            {
                p.ClientName = client.FullName;
                p.ClientId = client.Id;
            });
            response.Count = response.Patients.Count;

            return Ok(response);
        }
    }
}
