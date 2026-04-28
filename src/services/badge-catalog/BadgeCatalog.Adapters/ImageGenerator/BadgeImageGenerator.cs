using BadgeCatalog.Ports.Repositories;
using BadgeCatalog.Ports.Models;
using BadgeCatalog.Adapters.Templates;
using SkiaSharp;

namespace BadgeCatalog.Adapters.ImageGenerator;

public class BadgeImageGenerator : IBadgeImageGenerator
{
    public async Task<string> GenerateAsync(string templateId, BadgeRenderData data)
    {
        const int width = 400;
        const int height = 400;

        // 🔹 1. Resolver template
        var template = BadgeTemplateResolver.Get(templateId);

        // 🔹 2. Caminhos
        var templatePath = Path.Combine("wwwroot", "templates", template.BackgroundImage);
        var fontPath = Path.Combine("wwwroot", "fontes", "NotoSans-Bold.ttf");

        using var surface = SKSurface.Create(new SKImageInfo(width, height));
        var canvas = surface.Canvas;

        // 🔹 3. Desenhar background (template)
        if (File.Exists(templatePath))
        {
            using var bitmap = SKBitmap.Decode(templatePath);
            canvas.DrawBitmap(bitmap, new SKRect(0, 0, width, height));
        }
        else
        {
            canvas.Clear(SKColors.White);
        }
        // 🔹 3.1 Desenhar logo (se existir)
        var logoFile = data.LogoPath ?? template.DefaultLogoPath;

        if (!string.IsNullOrEmpty(logoFile))
        {
            var logoPath = Path.Combine("wwwroot", "logos", logoFile);

            if (File.Exists(logoPath))
            {
                using var logoBitmap = SKBitmap.Decode(logoPath);

                var size = template.LogoSize;

                var logoX = (width - size) / 2;
                var logoY = template.LogoYPosition;

                canvas.DrawBitmap(
                    logoBitmap,
                    new SKRect(logoX, logoY, logoX + size, logoY + size)
                );
            }
        }

        // 🔹 4. Configurar texto
        using var textPaint = new SKPaint
        {
            Color = SKColor.Parse(data.TextColor ?? template.DefaultTextColor),
            TextSize = template.DefaultFontSize,
            IsAntialias = true,
            TextAlign = SKTextAlign.Center,
            Style = SKPaintStyle.Fill,
            Typeface = File.Exists(fontPath)
                ? SKTypeface.FromFile(fontPath)
                : SKTypeface.Default
        };

        // 🔹 5. Auto resize
        var maxTextWidth = 280;

        while (textPaint.MeasureText(data.BadgeName) > maxTextWidth)
        {
            textPaint.TextSize -= 2;
        }

        // 🔹 6. Calcular posição
        float x = width / 2;

        var bounds = new SKRect();
        textPaint.MeasureText(data.BadgeName, ref bounds);

        float y = template.TextYPosition - bounds.MidY;

        // 🔹 7. Desenhar texto
        // canvas.DrawText(data.BadgeName, x, y, textPaint);

        var words = data.BadgeName.Split(' ');
        var lines = new List<string>();
        var currentLine = "";

        var maxWidth = 280;

        // 🔹 monta linhas automaticamente
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

        // 🔹 altura entre linhas
        float lineHeight = textPaint.TextSize + 5;

        // 🔹 centralizar bloco de texto
        float startY = template.TextYPosition - ((lines.Count - 1) * lineHeight / 2);

        for (int i = 0; i < lines.Count; i++)
        {
            var line = lines[i];

            var lineBounds = new SKRect();
            textPaint.MeasureText(line, ref lineBounds);

            float lineY = startY + (i * lineHeight) - lineBounds.MidY;

            canvas.DrawText(line, x, lineY, textPaint);
        }

        // 🔹 8. Exportar imagem
        using var image = surface.Snapshot();
        using var dataImage = image.Encode(SKEncodedImageFormat.Png, 100);

        var fileName = $"{Guid.NewGuid()}.png";
        var path = Path.Combine("wwwroot", "badges", fileName);

        Directory.CreateDirectory(Path.GetDirectoryName(path)!);

        await File.WriteAllBytesAsync(path, dataImage.ToArray());

        return $"/badges/{fileName}";
    }
}