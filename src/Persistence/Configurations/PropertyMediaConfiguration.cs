using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PropertyMediaConfiguration : IEntityTypeConfiguration<PropertyMedia>
{
    public void Configure(EntityTypeBuilder<PropertyMedia> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ObjectKey)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.MediaType)
            .HasMaxLength(100);

        builder.Property(x => x.Order)
            .IsRequired();

        builder.HasOne(x => x.PropertyAd)
            .WithMany(p => p.PropertyMedias)
            .HasForeignKey(x => x.PropertyAdId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
