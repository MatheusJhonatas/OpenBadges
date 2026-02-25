namespace BadgeCatalog.Api.Requests;

public class UpdateBadgeRequest
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}