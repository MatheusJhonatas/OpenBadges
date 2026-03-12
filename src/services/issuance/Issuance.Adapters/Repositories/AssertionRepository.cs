using Issuance.Domain.Aggregates;
using Issuance.Ports.Repositories;

namespace Issuance.Adapters.Repositories;


public class AssertionRepository : IAssertionRepository
{
    private static readonly List<Assertion> storage = new();
    public Task AddAsync(Assertion assertion, CancellationToken cancellationToken = default)
    {
        storage.Add(assertion);
        return Task.CompletedTask;
    }
}

