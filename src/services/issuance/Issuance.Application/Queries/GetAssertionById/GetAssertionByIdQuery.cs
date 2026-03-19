using MediatR;
using Issuance.Domain.Aggregates;
using Issuance.Application.Dtos;

namespace Issuance.Application.Queries.GetAssertionById;

// Query que representa a intenção de buscar uma assertion por ID
public record GetAssertionByIdQuery(Guid Id) : IRequest<AssertionResponse?>;