using BadgeCatalog.Ports.Repositories;
using BadgeCatalog.Ports.Models;
using BadgeCatalog.Adapters.Templates;
using SkiaSharp;
using Svg.Skia;

namespace BadgeCatalog.Adapters.ImageGenerator;

public class BadgeImageGenerator : IBadgeImageGenerator
{
    public async Task<byte[]> GenerateAsync(string templateId, BadgeRenderData data)
    {
        const int width = 1200;
        const int height = 1200;

        // 🔹 escala baseada no template original 400x400
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

        using var surface = SKSurface.Create(new SKImageInfo(width, height));
        var canvas = surface.Canvas;

        canvas.Clear(SKColors.Transparent);

        // 🔹 3. Renderizar SVG
        if (File.Exists(templatePath))
        {
            var svg = new SKSvg();
            svg.Load(templatePath);

            if (svg.Picture != null)
            {
                var picture = svg.Picture;

                var svgBounds = picture.CullRect;

                float svgScaleX = width / svgBounds.Width;
                float svgScaleY = height / svgBounds.Height;

                canvas.Save();

                canvas.Scale(svgScaleX, svgScaleY);

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
        var maxTextWidth = 850f;

        while (
            textPaint.MeasureText(data.BadgeName) > maxTextWidth
            && textPaint.TextSize > 40)
        {
            textPaint.TextSize -= 2;
        }

        // 🔹 6. Quebra automática de linha
        var words = data.BadgeName.Split(' ');

        var lines = new List<string>();

        var currentLine = "";

        var maxWidth = 850f;

        foreach (var word in words)
        {
            var testLine = string.IsNullOrEmpty(currentLine)
                ? word
                : currentLine + " " + word;

            if (textPaint.MeasureText(testLine) > maxWidth)
            {
                lines.Add(currentLine);
                currentLine = word;
            }
            else
            {
                currentLine = testLine;
            }
        }

        if (!string.IsNullOrEmpty(currentLine))
        {
            lines.Add(currentLine);
        }

        // 🔹 7. Centralização vertical
        float x = width / 2f;

        float lineHeight = textPaint.TextSize + (12 * scale);

        float startY =
            (template.TextYPosition * scale)
            - ((lines.Count - 1) * lineHeight / 2);

        // 🔹 8. Desenhar linhas
        for (int i = 0; i < lines.Count; i++)
        {
            var line = lines[i];

            var bounds = new SKRect();

            textPaint.MeasureText(line, ref bounds);

            float lineY =
                startY
                + (i * lineHeight)
                - bounds.MidY;

            canvas.DrawText(line, x, lineY, textPaint);
        }

        // 🔹 9. Exportar PNG
        using var image = surface.Snapshot();

        using var dataImage = image.Encode(
            SKEncodedImageFormat.Png,
            100);

        return dataImage.ToArray();
    }
}