using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.ManagementModule.Domain.Aggregates;
using VeterinaryClinic.ManagementModule.Shared.DTOs.Doctor;
using VeterinaryClinic.SharedKernel.Interfaces;

namespace VeterinaryClinic.ManagementModule.Api.Controllers
{
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IRepository<Doctor> _repository;
        private readonly IMapper _mapper;

        public DoctorController(IRepository<Doctor> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet(ListDoctorRequest.Route)]        
        public async Task<ActionResult<ListDoctorResponse>> List(
            [FromQuery] ListDoctorRequest request, CancellationToken cancellationToken)
        {
            var response = new ListDoctorResponse(request.CorrelationId);

            var doctors = await _repository.ListAsync();
            if (doctors is null) return NotFound();

            response.Doctors = _mapper.Map<List<DoctorDto>>(doctors);
            response.Count = response.Doctors.Count;

            return Ok(response);
        }
    }
}
