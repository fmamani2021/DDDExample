using AutoMapper;
using VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates;
using VeterinaryClinic.AppointmentModule.Shared.DTOs.Clients;

namespace VeterinaryClinic.AppointmentModule.Api.MappingProfile
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientDto>()
                .ForMember(dto => dto.ClientId, options => options.MapFrom(src => src.Id));
            CreateMap<ClientDto, Client>()
                .ForMember(dto => dto.Id, options => options.MapFrom(src => src.ClientId));
            CreateMap<CreateClientRequest, Client>();
            CreateMap<UpdateClientRequest, Client>()
                .ForMember(dto => dto.Id, options => options.MapFrom(src => src.ClientId));
            CreateMap<DeleteClientRequest, Client>();
            CreateMap<Patient, int>().ConvertUsing(src => src.Id);
        }
    }
}
