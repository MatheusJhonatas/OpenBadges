namespace BadgeCatalog.Adapters.Templates;

public static class BadgeTemplateResolver
{
    public static BadgeTemplate Get(string templateId)
    {
        return templateId switch
        {
            "fgv" => new BadgeTemplate
            {
                Id = "fgv",
                BackgroundImage = "badge-fgv.png",
                TextYPosition = 300
            },

            _ => new BadgeTemplate
            {
                Id = "default",
                BackgroundImage = "badge-default.png",
                TextYPosition = 300
            }
        };
    }
}