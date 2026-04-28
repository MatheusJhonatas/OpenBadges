namespace BadgeCatalog.Api.Requests;

    public sealed class UpdateBadgeClassRequest
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string TemplateId { get; set; } = default!;
    public string CriteriaNarrative { get; set; } = default!;
    public int Version { get; set; }
}
