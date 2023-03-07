using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.ManagementModule.Domain.ClientAgregate;
using VeterinaryClinic.ManagementModule.Domain.ClientAgregate.Specifications.Clients;
using VeterinaryClinic.ManagementModule.Shared.Common;
using VeterinaryClinic.ManagementModule.Shared.DTOs.Client;
using VeterinaryClinic.SharedKernel.Interfaces;

namespace VeterinaryClinic.ManagementModule.Api.Controllers
{
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IRepository<Client> _repository;
        private readonly IMapper _mapper;

        public ClientController(IRepository<Client> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet(ListClientRequest.Route)]
        public async Task<ActionResult<ListClientResponse>> Get(
            [FromQuery] ListClientRequest request, CancellationToken cancellationToken)
        {
            var response = new ListClientResponse(request.CorrelationId);

            var spec = new ClientsIncludePatientsSpec();
            var clients = await _repository.ListAsync(spec);
            if (clients is null) return NotFound();

            response.Clients = _mapper.Map<List<ClientDto>>(clients);
            response.Count = response.Clients.Count;

            return Ok(response);
        }

        [HttpGet(GetByIdClientRequest.Route)]        
        public async Task<ActionResult<GetByIdClientResponse>> GetById(
            [FromRoute] GetByIdClientRequest request, CancellationToken cancellationToken)
        {
            var response = new GetByIdClientResponse(request.CorrelationId);

            var client = await _repository.GetByIdAsync(request.ClientId);
            if (client is null) return NotFound();

            response.Client = _mapper.Map<ClientDto>(client);

            return Ok(response);
        }

        [HttpPost(CreateClientRequest.Route)]        
        public async Task<ActionResult<CreateClientResponse>> Create(
            CreateClientRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateClientResponse(request.CorrelationId);

            var toAdd = _mapper.Map<Client>(request);
            toAdd = await _repository.AddAsync(toAdd);

            var dto = _mapper.Map<ClientDto>(toAdd);
            response.Client = dto;

            return Ok(response);
        }

        [HttpPut(UpdateClientRequest.Route)]
        public async Task<ActionResult<UpdateClientResponse>> Update(
            UpdateClientRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new UpdateClientResponse(request.CorrelationId);

                var spec = new ClientsIncludePatientsSpec();
                var client = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
                if (client is null) return NotFound();


                client.UpdateName(request.FullName);

                foreach (var patientDto in request.Patients)
                {                    
                    if(patientDto.Status == ModelStatus.Create)
                        client.CreatePatient(patientDto.ClientId, patientDto.Name, patientDto.Sex, patientDto.Species, patientDto.Breed, patientDto.PreferredDoctorId);

                    if (patientDto.Status == ModelStatus.Update)
                        client.UpdatePatient(patientDto.PatientId,  patientDto.Name, patientDto.Sex, patientDto.Species, patientDto.Breed, patientDto.PreferredDoctorId);

                    if (patientDto.Status == ModelStatus.Delete)
                        client.DeletePatient(patientDto.PatientId);
                }


                await _repository.UpdateAsync(client);

                var dto = _mapper.Map<ClientDto>(client);
                response.Client = dto;

                // Note: These messages could be triggered from the Repository or DbContext events
                // In the DbContext you could look for entities marked with an interface saying they needed
                // to be synchronized via cross-domain events and publish the appropriate message.
                //var appEvent = new NamedEntityUpdatedEvent(_mapper.Map<NamedEntity>(toUpdate), "Client-Updated");
                //_messagePublisher.Publish(appEvent);

                return Ok(response);
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        [HttpDelete(DeleteClientRequest.Route)]        
        public async Task<ActionResult<DeleteClientResponse>> Delete(
            [FromRoute] DeleteClientRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteClientResponse(request.CorrelationId);

            var toDelete = _mapper.Map<Client>(request);
            await _repository.DeleteAsync(toDelete);

            return Ok(response);
        }

    }
}
