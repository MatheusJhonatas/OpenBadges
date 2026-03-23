using MediatR;

namespace Issuance.Application.Commands.ActiveAssertion;

public record ActiveAssertionCommand(Guid AssertionId) : IRequest<Unit>;