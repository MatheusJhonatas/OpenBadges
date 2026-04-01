namespace  Issuance.Application.Dtos;

public class AssertionResponse
{
    public Guid Id { get; set; }
    public Guid BadgeClassId  { get; set; }
    public string HashedEmail { get; set; } = string.Empty;
    public string RecipientName { get; set; } = string.Empty;
    public DateTime IssuedOn { get; set; }
    public int Status { get; set; }
}