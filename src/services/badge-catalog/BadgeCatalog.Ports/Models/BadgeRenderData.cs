namespace BadgeCatalog.Ports.Models;

public class BadgeRenderData
{
    public string BadgeName { get; set; } = default!;

    // branding dinâmico
    public string? LogoPath { get; set; }
    public string? TextColor { get; set; }
}