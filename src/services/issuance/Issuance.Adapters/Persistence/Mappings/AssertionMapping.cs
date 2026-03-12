using Issuance.Domain.Aggregates;
using Issuance.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Issuance.Adapters.Persistence.Mappings;

public sealed class AssertionMapping : IEntityTypeConfiguration<Assertion>
{
    public void Configure(EntityTypeBuilder<Assertion> builder)
    {
        builder.ToTable("Assertions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.BadgeClassId)
            .IsRequired();

        builder.Property(x => x.IssuedOn)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(x => x.RevokedOn);

        // Mapping do Value Object
        builder.OwnsOne(x => x.Recipient, recipient =>
        {
            recipient.Property(r => r.HashedEmail)
            .HasColumnName("RecipientHashedEmail")
            .IsRequired();
        });

        builder.Ignore(x => x.DomainEvents);
    }
}