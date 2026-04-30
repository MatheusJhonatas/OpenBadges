using Issuance.Application.Dtos;
using Issuance.Domain.Aggregates;
using Issuance.Ports.Clients;
using Issuance.Ports.Repositories;
using MediatR;

namespace Issuance.Application.Queries.GetAssertionById;

public class GetAssertionByIdHandler : IRequestHandler<GetAssertionByIdQuery, AssertionResponse?>
{
    private readonly IAssertionRepository _repository;
    private readonly IBadgeCatalogClient _badgeCatalogClient;
    public GetAssertionByIdHandler(IAssertionRepository repository, IBadgeCatalogClient badgeCatalogClient)
    {
        _repository = repository;
        _badgeCatalogClient = badgeCatalogClient;
    }
    public async Task<AssertionResponse?> Handle(GetAssertionByIdQuery request, CancellationToken cancellationToken)
    {
       var assertion = await _repository.GetByIdAsync(request.Id, cancellationToken);
       if (assertion == null) return null;
        var badge = await _badgeCatalogClient.GetByIdAsync(assertion.BadgeClassId, cancellationToken);
       return new AssertionResponse
       {
           Id = assertion.Id,
            BadgeClassId = assertion.BadgeClassId,
            HashedEmail = assertion.Recipient.HashedEmail,
            RecipientName = assertion.RecipientName,
            IssuedOn = assertion.IssuedOn,
            Status = (int)assertion.Status,
            
            Badge = badge == null ? null : new BadgeClassResponse
            {
                Name = badge.Name,
                Description = badge.Description,
                TemplateId = badge.TemplateId
            }
       };
    }
}