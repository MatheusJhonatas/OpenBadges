namespace Issuance.Domain.ValueObjects;

public sealed class RecipientIdentity
{
    public string HashedEmail { get; private set; } = default!;

    private RecipientIdentity() { } // EF Core

    public RecipientIdentity(string hashedEmail)
    {
        if (string.IsNullOrWhiteSpace(hashedEmail))
            throw new ArgumentException("Hashed email cannot be empty.", nameof(hashedEmail));

        HashedEmail = hashedEmail;
    }
}