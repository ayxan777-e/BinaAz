using Application.DTOs.Street;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class StreetProfile : Profile
{
    public StreetProfile()
    {
        CreateMap<CreateStreetRequest, Street>();
        CreateMap<UpdateStreetRequest, Street>();
        CreateMap<Street, GetAllStreetResponse>();
        CreateMap<Street, GetByIdStreetResponse>();
    }
}
