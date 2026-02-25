using BadgeCatalog.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BadgeCatalog.Adapters.Persistence.Mappings;

public class BadgeCatalogMap : IEntityTypeConfiguration<BadgeClass>
{
    public void Configure(EntityTypeBuilder<BadgeClass> builder)
    {
        builder.ToTable("BadgeClasses");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(b => b.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(b => b.Slug)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(b => b.IsActive)
            .IsRequired();

        builder.Property(b => b.Version)
            .IsRequired();

        builder.Property(b => b.CreatedAt)
            .IsRequired();

        builder.Property(b => b.UpdatedAt)
            .IsRequired();

        // Value Object - Image
        builder.OwnsOne(b => b.Image, image =>
        {
            image.Property(i => i.Url)
                .HasColumnName("ImageUrl")
                .IsRequired();
        });

        // Value Object - Criteria
        builder.OwnsOne(b => b.Criteria, criteria =>
        {
            criteria.Property(c => c.Narrative)
                .HasColumnName("CriteriaNarrative")
                .IsRequired();
        });
    }
}