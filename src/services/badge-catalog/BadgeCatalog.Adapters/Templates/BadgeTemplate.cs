namespace BadgeCatalog.Adapters.Templates;

public class BadgeTemplate
{
    public string Id { get; set; } = default!;

    // estrutura
    public string BackgroundImage { get; set; } = default!;
    public float TextYPosition { get; set; }

    // estilo padrão (fallback)
    public string DefaultTextColor { get; set; } = "#FFFFFF";
    public int DefaultFontSize { get; set; } = 40;
    public float LogoYPosition { get; set; } 
    public int LogoSize { get; set; } = 120;
}