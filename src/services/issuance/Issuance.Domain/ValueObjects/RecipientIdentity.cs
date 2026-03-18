using System.Security.Cryptography;
using System.Text;

namespace Issuance.Domain.ValueObjects;

public sealed class RecipientIdentity
{
    public string HashedEmail { get; private set; } = default!;

    private RecipientIdentity() { } // EF Core

    public RecipientIdentity(string hashedEmail)
    {
        HashedEmail = hashedEmail;
    }

    public static RecipientIdentity Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.", nameof(email));

        var hash = GenerateHash(email);

        return new RecipientIdentity(hash);
    }

    public static string GenerateHash(string email)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(email.Trim().ToLower());
        var hash = sha256.ComputeHash(bytes);

        return Convert.ToHexString(hash);
    }
}