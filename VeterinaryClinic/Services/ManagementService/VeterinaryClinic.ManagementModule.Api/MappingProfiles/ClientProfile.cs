﻿using AutoMapper;
using VeterinaryClinic.ManagementModule.Domain.ClientAgregate;
using VeterinaryClinic.ManagementModule.Shared.DTOs.Client;

namespace VeterinaryClinic.ManagementModule.Api.MappingProfiles
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
