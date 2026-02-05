using Application.Abstracts.Repositories;
using Application.Abstracts.Repositories.SimpleRepo;
using Application.Abstracts.Services;
using Application.Abstracts.Services.Simple;
using Application.Mapping;
using Application.Validations.City;
using Application.Validations.PropertyAd;
using Application.Validations.Street;
using FluentValidation;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repositories;
using Persistence.Repositories.Simple;
using Persistence.Services;

namespace API.Extensions;

public static class ServiceRegistration
{
    public static void AddMyServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<BinaLiteDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddValidatorsFromAssemblyContaining<CreatePropertyAdRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateCityRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateStreetRequestValidator>();

        services.AddAutoMapper(cfg => { },
                        typeof(PropertyAdProfile).Assembly,
                        typeof(CityProfile).Assembly);



        services.AddScoped(typeof(IRepository<,>), typeof(GenericRepository<,>));

        services.AddScoped<IPropertyAdService, PropertyAdServices>();
        services.AddScoped<ICityServices, CityService>();
        services.AddScoped<IStreetService, StreetService>();
        services.AddScoped<ICarsImageRepo, CarsImageRepository>();
        services.AddScoped<ICarsImageService, CarsImageService>();

        services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 50 * 1024 * 1024;
        });
    }
}
