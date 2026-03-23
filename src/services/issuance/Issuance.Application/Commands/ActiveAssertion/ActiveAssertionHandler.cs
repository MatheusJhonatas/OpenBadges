namespace Issuance.Application.Commands.ActiveAssertion;

using System.Threading;
using System.Threading.Tasks;
using Issuance.Ports.Repositories;
using MediatR;
public sealed class ActiveAssertionHandler : IRequestHandler<ActiveAssertionCommand, Unit>
{
    private readonly IAssertionRepository _repository;
    private readonly IMediator _mediator;
    public ActiveAssertionHandler(IAssertionRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }
    public async Task<Unit> Handle(ActiveAssertionCommand request, CancellationToken cancellationToken)
    {
       // 1. Buscar a assertion no banco
        var assertion = await _repository.GetByIdAsync(request.AssertionId, cancellationToken);

        // 2. Se não existir → erro de domínio (404 depois)
        if (assertion is null)
            throw new KeyNotFoundException($"Assertion {request.AssertionId} not found.");

        // 3. Executar regra de domínio (ativação)
        assertion.SetActive();
        await _repository.UpdateAsync(assertion, cancellationToken);

        foreach (var domainEvent in assertion.DomainEvents)
        {
           await _mediator.Publish(domainEvent, cancellationToken);
        }

        assertion.ClearDomainEvents();
        
        return Unit.Value;
    }
}
