using Issuance.Ports.Repositories;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Issuance.Application.Commands.RevokeAssertion;

public class RevokeAssertionHandler : IRequestHandler<RevokeAssertionCommand, Unit>
{
    private readonly IAssertionRepository _repository;
    private readonly IMediator _mediator;
        public RevokeAssertionHandler(IAssertionRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }
    public async Task<Unit> Handle(RevokeAssertionCommand request, CancellationToken cancellationToken)
    {
        
        // 1. Buscar a assertion no banco
        var assertion = await _repository.GetByIdAsync(request.AssertionId, cancellationToken);

        // 2. Se não existir → erro de domínio (404 depois)
        if (assertion is null)
            throw new KeyNotFoundException($"Assertion {request.AssertionId} not found.");

        // 3. Executar regra de domínio (revogação)
        assertion.Revoke();

        // 4. Persistir alteração
        await _repository.UpdateAsync(assertion, cancellationToken);

        var events = assertion.DomainEvents;

        foreach (var domainEvent in assertion.DomainEvents)
        {
           await _mediator.Publish(domainEvent, cancellationToken);
        }

        assertion.ClearDomainEvents();

        // 5. Retornar Unit (sem conteúdo)
        return Unit.Value;
    }
}