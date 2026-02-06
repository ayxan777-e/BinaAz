using Domain.Entities;
using Microsoft.AspNetCore.Http;

public interface IPropertyMediaService
{
    Task<int> UploadAsync(int propertyAdId, IFormFile file, int? order = null);
    Task DeleteAsync(int mediaId);
    Task<List<PropertyMedia>> GetByPropertyIdAsync(int propertyAdId);
}
