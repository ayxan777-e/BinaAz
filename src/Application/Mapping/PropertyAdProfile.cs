using Application.DTOs.PropertyAd;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class PropertyAdProfile: Profile
{
    public PropertyAdProfile()
    {
        CreateMap<PropertyAd, GetAllPropertyAdResponse>();
        CreateMap<PropertyAd, GetByIdPropertyAdResponse>();
        CreateMap<CreatePropertyAdRequest, PropertyAd>();
        CreateMap<UpdatePropertyAdRequest, PropertyAd>();
    }
}
