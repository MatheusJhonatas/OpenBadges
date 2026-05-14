namespace Issuance.Application.Dtos;

public class GetAssertionByVerificationCodeResponse
{
    public Guid Id { get; set; }

    public string VerificationCode { get; set; }
        = string.Empty;

    public string RecipientName { get; set; }
        = string.Empty;

    public Guid BadgeClassId { get; set; }

    public string Status { get; set; }
        = string.Empty;

    public DateTime IssuedOn { get; set; }

    public DateTime? RevokedOn { get; set; }
}