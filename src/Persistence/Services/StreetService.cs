using Application.Abstracts.Repositories;
using Application.Abstracts.Services;
using Application.DTOs.Street;
using AutoMapper;
using Domain.Entities;
using FluentValidation;

namespace Persistence.Services;

public class StreetService : IStreetService
{
    private readonly IRepository<Street, int> _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateStreetRequest> _createValidator;

    public StreetService(
        IRepository<Street, int> repository,
        IMapper mapper,
        IValidator<CreateStreetRequest> createValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _createValidator = createValidator;
    }

    public async Task<bool> CreateAsync(CreateStreetRequest request)
    {
        var validationResult = await _createValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var street = _mapper.Map<Street>(request);

        await _repository.AddAsync(street);
        await _repository.SaveChanges();
        return true;
    }

    public async Task<List<GetAllStreetResponse>> GetAllAsync()
    {
        var streets = await _repository.GetAllAsync();
        return _mapper.Map<List<GetAllStreetResponse>>(streets);
    }

    public async Task<GetByIdStreetResponse> GetByIdAsync(int id)
    {
        var street = await _repository.GetByIdAsync(id);

        if (street == null)
            throw new KeyNotFoundException("Street tapilmadi");

        return _mapper.Map<GetByIdStreetResponse>(street);
    }

    public async Task<bool> UpdateAsync(int id, UpdateStreetRequest request)
    {
        var street = await _repository.GetByIdAsync(id);

        if (street == null)
            throw new KeyNotFoundException("Street tapilmadi");

        _mapper.Map(request, street);

        await _repository.UpdateAsync(street);
        await _repository.SaveChanges();

        return true;
    }
}
