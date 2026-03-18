namespace Issuance.Domain.Exceptions;

public class DuplicateAssertionException : Exception
{
    public DuplicateAssertionException(Guid badgeId, string email)
        : base($"An assertion for badge {badgeId} already exists for email {email}.")
    {
    }
}