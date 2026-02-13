using System.Text;
using Application.Abstracts.Auth;
using Application.Abstracts.Repositories;
using Application.Abstracts.Repositories.SimpleRepo;
using Application.Abstracts.Services;
using Application.Abstracts.Services.Simple;
using Application.Common;
using Application.Mapping;
using Application.Validations.City;
using Application.Validations.PropertyAd;
using Application.Validations.Street;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Extension;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter: Bearer {your JWT token}"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

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
        services.AddScoped<IPropertyMediaService, PropertyMediaService>();


        services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 50 * 1024 * 1024;
        });
        services.AddMinioInfrastructure(configuration);

        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

        var jwtOptions = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>()
            ?? throw new InvalidOperationException("Jwt configuration is missing.");

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = signingKey,
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddAuthorization();

        services.AddIdentity<User, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 4;
        })
           .AddEntityFrameworkStores<BinaLiteDbContext>()
           .AddDefaultTokenProviders();

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IAuthService, AuthService>();
    }
}
