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

                TextYPosition = 210,

                TitleAreaTop = 140,
                TitleAreaHeight = 110,

                DefaultTextColor = "#070707",
                DefaultFontSize = 28,

            },
            "template1_logo_NTT" => new BadgeTemplate
            {
                Id = "template1_logo_NTT",
                BackgroundImage = "template1_logo_ntt.png",

                TextYPosition = 210,

                TitleAreaTop = 140,
                TitleAreaHeight = 110,

                DefaultTextColor = "#070707",
                DefaultFontSize = 28,

            },

            "template2" => new BadgeTemplate
            {
                Id = "template2",
                BackgroundImage = "template2.png",
                TextYPosition = 185,
                DefaultTextColor = "#080808",
                DefaultFontSize = 28,
                LogoYPosition = 220
            },
            "template2_logo_NTT" => new BadgeTemplate
            {
                Id = "template2_logo_NTT",
                BackgroundImage = "template2_logo_ntt.png",
                TextYPosition = 185,
                DefaultTextColor = "#080808",
                DefaultFontSize = 28,
                LogoYPosition = 220
            },
            "default_logo_ntt" => new BadgeTemplate
            {
                Id = "default_logo_ntt",
                BackgroundImage = "default_logo_ntt.png",
                TextYPosition = 210,
                DefaultTextColor = "#0c0c0c",
                DefaultFontSize = 28,
                LogoYPosition = 60
            },

            _ => new BadgeTemplate
            {
                Id = "default",
                BackgroundImage = "default.png",
                TextYPosition = 210,
                DefaultTextColor = "#0c0c0c",
                DefaultFontSize = 28,
                LogoYPosition = 60
            }
        };
    }
}