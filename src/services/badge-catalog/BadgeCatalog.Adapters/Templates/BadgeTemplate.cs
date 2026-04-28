namespace BadgeCatalog.Adapters.Templates;

public class BadgeTemplate
{
    public string Id { get; set; } = default!;
    public string BackgroundImage { get; set; } = default!;

    // ❗ antigos (mantém por enquanto)
    public float TextYPosition { get; set; }
    public float LogoYPosition { get; set; }

    public float TitleAreaTop { get; set; }
    public float TitleAreaHeight { get; set; }

    public string DefaultTextColor { get; set; } = "#FFFFFF";
    public int DefaultFontSize { get; set; } = 40;
}