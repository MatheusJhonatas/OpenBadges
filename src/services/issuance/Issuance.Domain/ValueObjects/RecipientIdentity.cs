using System.Security.Cryptography;
using System.Text;

namespace Issuance.Domain.ValueObjects;
public sealed class RecipientIdentity
{
    public string Email { get; private set; } = default!;

    public string HashedEmail { get; private set; } = default!;

    private RecipientIdentity() { }

    private RecipientIdentity(
        string email,
        string hashedEmail)
    {
        Email = email;

        HashedEmail = hashedEmail;
    }

    public static RecipientIdentity Create(
        string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException(
                "Email cannot be empty.",
                nameof(email));

        var normalizedEmail =
            email.Trim().ToLower();

        var hash =
            GenerateHash(normalizedEmail);

        return new RecipientIdentity(
            normalizedEmail,
            hash);
    }

    public static string GenerateHash(
        string email)
    {
        using var sha256 = SHA256.Create();

        var bytes =
            Encoding.UTF8.GetBytes(email);

        var hash =
            sha256.ComputeHash(bytes);

        return Convert.ToHexString(hash);
    }
}