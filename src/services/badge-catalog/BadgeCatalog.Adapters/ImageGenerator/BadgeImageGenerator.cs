using BadgeCatalog.Ports.Repositories;
using BadgeCatalog.Ports.Models;
using BadgeCatalog.Adapters.Templates;
using SkiaSharp;
using Svg.Skia;

namespace BadgeCatalog.Adapters.ImageGenerator;

public class BadgeImageGenerator : IBadgeImageGenerator
{
    public async Task<byte[]> GenerateAsync(
        string templateId,
        BadgeRenderData data)
    {
        const int width = 1200;
        const int height = 1200;

        // 🔹 escala baseada no template original
        const float baseSize = 400f;

        float scale = width / baseSize;

        // 🔹 1. Resolver template
        var template = BadgeTemplateResolver.Get(templateId);

        // 🔹 2. Caminhos
        var templatePath = Path.Combine(
            "wwwroot",
            "templates",
            template.BackgroundImage);

        var fontPath = Path.Combine(
            "wwwroot",
            "fontes",
            "NotoSans-Bold.ttf");

        using var surface = SKSurface.Create(
            new SKImageInfo(width, height));

        var canvas = surface.Canvas;

        // 🔹 fundo branco
        canvas.Clear(SKColors.White);

        // 🔹 3. Renderizar SVG
        if (File.Exists(templatePath))
        {
            var svg = new SKSvg();

            svg.Load(templatePath);

            var picture = svg.Picture;

            if (picture != null)
            {
                var svgBounds = picture.CullRect;

                float scaleX =
                    width / svgBounds.Width;

                float scaleY =
                    height / svgBounds.Height;

                float svgScale =
                    Math.Min(scaleX, scaleY);

                float translateX =
                    (width - (svgBounds.Width * svgScale)) / 2f;

                float translateY =
                    (height - (svgBounds.Height * svgScale)) / 2f;

                canvas.Save();

                canvas.Translate(
                    translateX,
                    translateY);

                canvas.Scale(svgScale);

                canvas.DrawPicture(picture);

                canvas.Restore();
            }
        }
        else
        {
            canvas.Clear(SKColors.White);
        }

        // 🔹 4. Configurar texto
        using var textPaint = new SKPaint
        {
            Color = SKColor.Parse(
                data.TextColor ?? template.DefaultTextColor),

            TextSize = template.DefaultFontSize * scale,

            IsAntialias = true,

            TextAlign = SKTextAlign.Center,

            Style = SKPaintStyle.Fill,

            Typeface = File.Exists(fontPath)
                ? SKTypeface.FromFile(fontPath)
                : SKTypeface.Default
        };

        // 🔹 5. Auto resize
        const float maxTextWidth = 850f;

        while (
            textPaint.MeasureText(data.BadgeName) > maxTextWidth
            && textPaint.TextSize > 40)
        {
            textPaint.TextSize -= 2;
        }

        // 🔹 6. Quebra automática
        var words = data.BadgeName.Split(' ');

        var lines = new List<string>();

        var currentLine = "";

        foreach (var word in words)
        {
            var testLine = string.IsNullOrWhiteSpace(currentLine)
                ? word
                : $"{currentLine} {word}";

            if (textPaint.MeasureText(testLine) > maxTextWidth)
            {
                lines.Add(currentLine);

                currentLine = word;
            }
            else
            {
                currentLine = testLine;
            }
        }

        if (!string.IsNullOrWhiteSpace(currentLine))
        {
            lines.Add(currentLine);
        }

        // 🔹 7. Posicionamento
        float x = width / 2f;

        float lineHeight =
            textPaint.TextSize + (12 * scale);

        float startY =
            (template.TextYPosition * scale)
            - ((lines.Count - 1) * lineHeight / 2);

        // 🔹 8. Renderizar texto
        for (int i = 0; i < lines.Count; i++)
        {
            var line = lines[i];

            var bounds = new SKRect();

            textPaint.MeasureText(line, ref bounds);

            float lineY =
                startY
                + (i * lineHeight)
                - bounds.MidY;

            canvas.DrawText(
                line,
                x,
                lineY,
                textPaint);
        }

        // 🔹 9. Exportar PNG
        using var image = surface.Snapshot();

        using var dataImage = image.Encode(
            SKEncodedImageFormat.Png,
            100);

        return dataImage.ToArray();
    }
}