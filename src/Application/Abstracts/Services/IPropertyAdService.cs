using Application.DTOs.PropertyAd;

namespace Application.Abstracts.Services;

public interface IPropertyAdService
{
    Task<List<GetAllPropertyAdResponse>> GetAllAsync();
    Task<GetByIdPropertyAdResponse> GetByIdAsync(int id);
    Task CreateAsync(CreatePropertyAdRequest request);

}
