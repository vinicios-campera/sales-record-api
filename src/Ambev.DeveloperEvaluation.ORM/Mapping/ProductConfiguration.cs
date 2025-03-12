using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(u => u.Title).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Price).IsRequired();
        builder.Property(u => u.Description).IsRequired().HasMaxLength(150);

        builder.Property(u => u.Category)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(u => u.Image).IsRequired().HasMaxLength(200);

        builder.OwnsOne(p => p.Rating, ratingBuilder =>
        {
            ratingBuilder.Property(r => r.Rate).IsRequired().HasColumnName("Rate");
            ratingBuilder.Property(r => r.Count).IsRequired().HasColumnName("Count");
        });
    }
}