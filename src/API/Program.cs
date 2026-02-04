using API.Middlewares;
using Application.Abstracts.Repositories;
using Application.Abstracts.Repositories.SimpleRepo;
using Application.Abstracts.Services;
using Application.Abstracts.Services.Simple;
using Application.DTOs.Street;
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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<BinaLiteDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddValidatorsFromAssemblyContaining<CreatePropertyAdRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCityRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateStreetRequestValidator>();
builder.Services.AddAutoMapper(cfg => { }, typeof(PropertyAdProfile).Assembly);
builder.Services.AddAutoMapper(cfg => { }, typeof(CityProfile).Assembly);

builder.Services.AddScoped(typeof(IRepository<,>),
                           typeof(GenericRepository<,>));

builder.Services.AddScoped<IPropertyAdService, PropertyAdServices>();
builder.Services.AddScoped<ICityServices, CityService>();
builder.Services.AddScoped<IStreetService, StreetService>();
builder.Services.AddScoped<ICarsImageRepo, CarsImageRepository>();
builder.Services.AddScoped<ICarsImageService, CarsImageService>();
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 50 * 1024 * 1024;
});

var app = builder.Build();

app.UseExceptionHandling();
app.UseRequestResponseLogging();
app.UseStaticFiles();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
