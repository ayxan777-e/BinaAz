using Application.Abstracts.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Services;

public class PropertyMediaService : IPropertyMediaService
{
    private readonly IRepository<PropertyMedia, int> _repository;
    private readonly IFileStorageService _storage;

    public PropertyMediaService(
        IRepository<PropertyMedia, int> repository,
        IFileStorageService storage)
    {
        _repository = repository;
        _storage = storage;
    }

    public async Task<int> UploadAsync(int propertyAdId, IFormFile file, int? order = null)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File boş ola bilməz");

        using var stream = file.OpenReadStream();

        var objectKey = await _storage.SaveAsync(
            stream,
            file.FileName,
            file.ContentType);

        var media = new PropertyMedia
        {
            PropertyAdId = propertyAdId,
            ObjectKey = objectKey,
            MediaType = file.ContentType,
            Order = order ?? 1
        };

        await _repository.AddAsync(media);
        await _repository.SaveChanges();

        return media.Id;
    }

    public async Task DeleteAsync(int mediaId)
    {
        var media = await _repository.GetByIdAsync(mediaId);
        if (media == null) return;

        await _storage.DeleteFileAsync(media.ObjectKey);

        await _repository.DeleteAsync(media);
        await _repository.SaveChanges();
    }

    public async Task<List<PropertyMedia>> GetByPropertyIdAsync(int propertyAdId)
    {
        return await _repository.Query()
            .Where(x => x.PropertyAdId == propertyAdId)
            .OrderBy(x => x.Order)
            .ToListAsync();
    }
}
