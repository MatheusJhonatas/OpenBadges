
using Issuance.Ports.Dtos;

namespace Issuance.Ports.Clients;

public interface IBadgeCatalogClient
{
    Task<bool> BadgeExistsAsync(Guid badgeClassId, CancellationToken cancellationToken);
    Task<BadgeDto?> GetByIdAsync(Guid badgeClassId, CancellationToken cancellationToken);
}