namespace BadgeCatalog.Adapters.Templates;

public static class BadgeTemplateResolver
{
    public static BadgeTemplate Get(string templateId)
    {
        return templateId switch
        {
           "template1" => new BadgeTemplate
            {
                Id = "template1",
                BackgroundImage = "template1.png",
                TextYPosition = 190,
                DefaultTextColor = "#070707",
                DefaultFontSize = 28,
                LogoYPosition = 220,
                LogoSize = 120
            },

            "template2" => new BadgeTemplate
            {
                Id = "template2",
                BackgroundImage = "template2.png",
                TextYPosition = 185,
                DefaultTextColor = "#080808",
                DefaultFontSize = 28,
                LogoYPosition = 220,
                LogoSize = 120
            },

            _ => new BadgeTemplate
            {
                Id = "default",
                BackgroundImage = "default.png",
                TextYPosition = 210,
                DefaultTextColor = "#0c0c0c",
                DefaultFontSize = 28,
                LogoYPosition = 220,
                LogoSize = 120
            }
        };
    }
}