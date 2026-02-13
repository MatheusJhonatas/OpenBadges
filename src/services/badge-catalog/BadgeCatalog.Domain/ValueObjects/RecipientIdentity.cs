namespace BadgeCatalog.Domain.ValueObjects;

public sealed class RecipientIdentity
{
    public string HashedEmail { get;  }

    private RecipientIdentity() { } // For EF Core

    public RecipientIdentity(string hashedEmail)
    {
        if (string.IsNullOrWhiteSpace(hashedEmail))
            throw new ArgumentException("Hashed email cannot be empty.", nameof(hashedEmail));
        HashedEmail = hashedEmail;
    }
}