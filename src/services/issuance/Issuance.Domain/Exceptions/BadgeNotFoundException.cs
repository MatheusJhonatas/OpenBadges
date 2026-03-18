namespace Issuance.Domain.Exceptions;

public class BadgeNotFoundException : Exception
{
    public BadgeNotFoundException(Guid badgeId)
        : base($"Badge with id '{badgeId}' was not found.")
    {
    }
}