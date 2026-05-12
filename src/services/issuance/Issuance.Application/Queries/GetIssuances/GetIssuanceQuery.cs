using MediatR;
using Issuance.Application.Dtos;

namespace Issuance.Application.Queries.GetIssuances;

public record GetIssuancesQuery()
    : IRequest<List<GetIssuancesResponse>>;