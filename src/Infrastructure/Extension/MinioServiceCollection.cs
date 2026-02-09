using Application.Common;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Minio;

namespace Infrastructure.Extension;

public static class MinioServiceCollection
{
    public static IServiceCollection AddMinioInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MinioOptions>(
            configuration.GetSection("Minio"));

        services.AddSingleton<IMinioClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<MinioOptions>>().Value;

            return new MinioClient()
                .WithEndpoint(options.Endpoint)
                .WithCredentials(options.AccessKey, options.SecretKey)
                .WithSSL(options.UseSSL)
                .Build();
        });

        services.AddScoped<IFileStorageService, MinioFileStorageService>();

        return services;
    }
}
