namespace Issuance.Ports.Clients;

public interface IBadgeCatalogClient
{
    Task<bool> BadgeExistsAsync(Guid badgeClassId, CancellationToken cancellationToken);
}