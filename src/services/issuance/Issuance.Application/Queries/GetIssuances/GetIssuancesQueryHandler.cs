using Issuance.Application.Dtos;
using Issuance.Ports.Repositories;
using MediatR;

namespace Issuance.Application.Queries.GetIssuances;

public class GetIssuancesQueryHandler : IRequestHandler<GetIssuancesQuery, List<GetIssuancesResponse>>
{
    private readonly IAssertionRepository _assertionRepository;

    public GetIssuancesQueryHandler(IAssertionRepository assertionRepository)
    {
        _assertionRepository = assertionRepository;
    }

    public async Task<List<GetIssuancesResponse>> Handle(GetIssuancesQuery request, CancellationToken cancellationToken)
    {
        var assertions = await _assertionRepository.GetAllAsync(cancellationToken);

        return assertions.Select(x => new GetIssuancesResponse
        {
            Id = x.Id,

            RecipientName = x.RecipientName,

            RecipientEmail =
                x.Recipient.Email,

            BadgeClassId = x.BadgeClassId,

            Status = x.Status.ToString(),

            IssuedOn = x.IssuedOn,

            RevokedOn = x.RevokedOn
        }).ToList();
    }
}