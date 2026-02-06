public interface IFileStorageService
{
    Task<string> SaveAsync(Stream fileStream, string fileName, string contentType);
    Task DeleteFileAsync(string objectKey);
}
