using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates;
using VeterinaryClinic.AppointmentModule.Shared.DTOs.Doctors;
using VeterinaryClinic.SharedKernel.Interfaces;

namespace VeterinaryClinic.AppointmentModule.Api.Controllers
{
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IReadRepository<Doctor> _repository;
        private readonly IMapper _mapper;

        public DoctorController(IReadRepository<Doctor> repository,
          IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet(ListDoctorRequest.Route)]        
        public async Task<ActionResult<ListDoctorResponse>> List(
            [FromQuery] ListDoctorRequest request,
            CancellationToken cancellationToken)
        {
            var response = new ListDoctorResponse(request.CorrelationId());

            var doctors = await _repository.ListAsync();
            if (doctors is null) return NotFound();

            response.Doctors = _mapper.Map<List<DoctorDto>>(doctors);
            response.Count = response.Doctors.Count;

            return Ok(response);
        }

        [HttpGet(GetByIdDoctorRequest.Route)]        
        public async Task<ActionResult<GetByIdDoctorResponse>> GetById(
            [FromRoute] GetByIdDoctorRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdDoctorResponse(request.CorrelationId());

            var doctor = await _repository.GetByIdAsync(request.DoctorId);
            if (doctor is null) return NotFound();

            response.Doctor = _mapper.Map<DoctorDto>(doctor);

            return Ok(response);
        }
    }
}
