using Issuance.Domain.Aggregates;
using Issuance.Ports.Repositories;

namespace Issuance.Adapters.Repositories;


public class AssertionRepository : IAssertionRepository
{
    public Task AddAsync(Assertion assertion, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

