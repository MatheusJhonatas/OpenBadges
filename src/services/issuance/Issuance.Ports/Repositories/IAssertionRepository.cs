using Issuance.Domain.Aggregates;

namespace Issuance.Ports.Repositories;

public interface IAssertionRepository
{
    Task AddAsync(Assertion assertion, CancellationToken cancellationToken = default);
}