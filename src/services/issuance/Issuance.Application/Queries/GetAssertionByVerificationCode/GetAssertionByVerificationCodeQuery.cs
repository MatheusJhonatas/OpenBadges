using MediatR;
using Issuance.Application.Dtos;
namespace Issuance.Application.Queries.GetAssertionByVerificationCode;

public record GetAssertionByVerificationCodeQuery(
    string VerificationCode)
    : IRequest<GetAssertionByVerificationCodeResponse?>;