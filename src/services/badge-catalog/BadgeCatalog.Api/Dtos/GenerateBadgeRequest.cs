namespace BadgeCatalog.Api.Dtos;

public class GenerateBadgeRequest
{
    public string TemplateId { get; set; } = default!;
    public string Name { get; set; } = default!;
}