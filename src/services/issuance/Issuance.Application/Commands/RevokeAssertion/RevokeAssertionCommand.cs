using MediatR;

namespace Issuance.Application.Commands.RevokeAssertion;

public record  RevokeAssertionCommand (Guid AssertionId) : IRequest<Unit>;
