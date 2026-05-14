using Issuance.Application.Dtos;
using Issuance.Ports.Repositories;
using MediatR;

namespace Issuance.Application.Queries
    .GetAssertionByVerificationCode;

public class GetAssertionByVerificationCodeHandler
    : IRequestHandler<
        GetAssertionByVerificationCodeQuery,
        GetAssertionByVerificationCodeResponse?>
{
    private readonly IAssertionRepository _repository;

    public GetAssertionByVerificationCodeHandler(
        IAssertionRepository repository)
    {
        _repository = repository;
    }

    public async Task<
        GetAssertionByVerificationCodeResponse?>
        Handle(
            GetAssertionByVerificationCodeQuery request,
            CancellationToken cancellationToken)
    {
        var assertion =
            await _repository
                .GetByVerificationCodeAsync(
                    request.VerificationCode,
                    cancellationToken);

        if (assertion is null)
        {
            return null;
        }

        return new GetAssertionByVerificationCodeResponse
        {
            Id = assertion.Id,

            VerificationCode =
                assertion.VerificationCode,

            RecipientName =
                assertion.RecipientName,

            BadgeClassId =
                assertion.BadgeClassId,

            Status =
                assertion.Status.ToString(),

            IssuedOn =
                assertion.IssuedOn,

            RevokedOn =
                assertion.RevokedOn
        };
    }
}