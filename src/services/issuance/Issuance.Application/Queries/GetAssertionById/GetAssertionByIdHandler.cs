using Issuance.Domain.Aggregates;
using Issuance.Ports.Repositories;
using MediatR;

namespace Issuance.Application.Queries.GetAssertionById;

public class GetAssertionByIdHandler : IRequestHandler<GetAssertionByIdQuery, Assertion?>
{
    private readonly IAssertionRepository _repository;
    public GetAssertionByIdHandler(IAssertionRepository repository)
    {
        _repository = repository;
    }
    public async Task<Assertion?> Handle(GetAssertionByIdQuery request, CancellationToken cancellationToken)
    {
       var assertion = await _repository.GetByIdAsync(request.Id, cancellationToken);
       return assertion;
    }
}