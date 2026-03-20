using Issuance.Domain.Aggregates;

namespace Issuance.Ports.Repositories;

public interface IAssertionRepository
{
    Task AddAsync(Assertion assertion, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid badgeId, string email, CancellationToken cancellationToken);
    Task<Assertion?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateAsync(Assertion assertion, CancellationToken cancellationToken);
}