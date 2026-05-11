namespace BadgeCatalog.Adapters.Templates;

public static class BadgeTemplateResolver
{
    public static BadgeTemplate Get(string templateId)
    {
        return templateId switch
        {
            "template-1" => new BadgeTemplate
            {
                Id = "template-1",
                BackgroundImage = "template-1.svg",

                TextYPosition = 220,

                TitleAreaTop = 140,
                TitleAreaHeight = 110,

                DefaultTextColor = "#070707",
                DefaultFontSize = 28,
            },

            "template-2" => new BadgeTemplate
            {
                Id = "template-2",
                BackgroundImage = "template-2.svg",

                TextYPosition = 185,

                DefaultTextColor = "#080808",
                DefaultFontSize = 28,

                LogoYPosition = 220
            },

            "template-3" => new BadgeTemplate
            {
                Id = "template-3",
                BackgroundImage = "template-3.svg",

                TextYPosition = 185,

                DefaultTextColor = "#111111",
                DefaultFontSize = 28,

                LogoYPosition = 200
            },

            "template-4" => new BadgeTemplate
            {
                Id = "template-4",
                BackgroundImage = "template-4.svg",

                TextYPosition = 220,

                DefaultTextColor = "#0c0c0c",
                DefaultFontSize = 28,

                LogoYPosition = 60
            },

            _ => new BadgeTemplate
            {
                Id = "default",
                BackgroundImage = "default.svg",

                TextYPosition = 208,

                DefaultTextColor = "#0c0c0c",
                DefaultFontSize = 28,

                LogoYPosition = 60
            }
        };
    }
}