using Domain.Entities;
using Domain.Entities.Simple;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context;

public class BinaLiteDbContext: IdentityDbContext<User>
{
    public BinaLiteDbContext(DbContextOptions<BinaLiteDbContext> options) : base(options)
    {
    }
    
    public DbSet<PropertyAd> PropertyAds { get; set; } = null!;
    public DbSet<PropertyMedia> PropertyMedias { get; set; } = null!;
    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<Street> Streets { get; set; } = null!;
    public DbSet<CarsImage> CarsImages { get; set; }=null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
      
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BinaLiteDbContext).Assembly);
    }
}
