using Application.Abstracts.Repositories;
using Application.Abstracts.Services;
using Application.DTOs.PropertyAd;
using AutoMapper;
using Domain.Entities;

namespace Persistence.Services;

public class PropertyAdServices : IPropertyAdService
{
    private readonly IRepository<PropertyAd, int> _repository;
    private readonly IMapper _mapper;

    public PropertyAdServices(IRepository<PropertyAd, int> repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task CreateAsync(CreatePropertyAdRequest request)
    {
        var entity = _mapper.Map<PropertyAd>(request);
        await _repository.AddAsync(entity);
        await _repository.SaveChanges();
    }

    public async Task<List<GetAllPropertyAdResponse>> GetAllAsync()
    {
        var entities =  await _repository.GetAllAsync();
        return _mapper.Map<List<GetAllPropertyAdResponse>>(entities);
    }

    public async Task<GetByIdPropertyAdResponse> GetByIdAsync(int id)
    {
        var entity = await  _repository.GetByIdAsync(id);
        return _mapper.Map<GetByIdPropertyAdResponse>(entity);
    }
}