using AutoMapper;
using VeterinaryClinic.ManagementModule.Domain.ClientAgregate;
using VeterinaryClinic.ManagementModule.Shared.DTOs.Patient;

namespace VeterinaryClinic.ManagementModule.Api.MappingProfiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, PatientDto>()
                .ForMember(dto => dto.PatientId, options => options.MapFrom(src => src.Id))
                .ForMember(dto => dto.Species, options => options.MapFrom(src => src.AnimalType.Species))
                .ForMember(dto => dto.Breed, options => options.MapFrom(src => src.AnimalType.Breed));

            CreateMap<PatientDto, Patient>()
                .ForMember(dto => dto.Id, options => options.MapFrom(src => src.PatientId))
                .ForPath(dto => dto.AnimalType.Species, options => options.MapFrom(src => src.Species))
                .ForPath(dto => dto.AnimalType.Breed, options => options.MapFrom(src => src.Breed));

            CreateMap<CreatePatientRequest, Patient>();
            
            CreateMap<UpdatePatientRequest, Patient>()
                .ForMember(dto => dto.Id, options => options.MapFrom(src => src.PatientId));

            CreateMap<DeletePatientRequest, Patient>();
        }
    }
}
