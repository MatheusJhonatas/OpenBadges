namespace BadgeCatalog.Adapters.Templates;

public static class BadgeTemplateResolver
{
    public static BadgeTemplate Get(string templateId)
    {
        return templateId switch
        {
           "circular-basic" => new BadgeTemplate
            {
                Id = "circular-basic",
                BackgroundImage = "circular-basic.png",
                TextYPosition = 280,
                DefaultTextColor = "#FFFFFF",
                DefaultFontSize = 40
            },

            "circular-stripe" => new BadgeTemplate
            {
                Id = "circular-stripe",
                BackgroundImage = "circular-stripe.png",
                TextYPosition = 300,
                DefaultTextColor = "#FFFFFF",
                DefaultFontSize = 40
            },

            _ => new BadgeTemplate
            {
                Id = "default",
                BackgroundImage = "default.png",
                TextYPosition = 300,
                DefaultTextColor = "#FFFFFF",
                DefaultFontSize = 40
            }
        };
    }
}