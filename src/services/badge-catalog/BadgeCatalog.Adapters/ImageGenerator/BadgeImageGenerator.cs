using BadgeCatalog.Adapters.Templates;
using BadgeCatalog.Ports.Models;
using BadgeCatalog.Ports.Repositories;
using SkiaSharp;

namespace BadgeCatalog.Adapters.ImageGenerator;

public class BadgeImageGenerator : IBadgeImageGenerator
{
    public async Task<string> GenerateAsync(string templateId, BadgeRenderData renderData)
    {
        const int width = 400;
        const int height = 400;

        var templatePath = Path.Combine("wwwroot", "templates", "badge-base.png");
        var fontPath = Path.Combine("wwwroot", "fontes", "NotoSans-Bold.ttf");

        using var surface = SKSurface.Create(new SKImageInfo(width, height));
        var canvas = surface.Canvas;

        // 🔥 1. DESENHA TEMPLATE BASE
        if (File.Exists(templatePath))
        {
            using var templateBitmap = SKBitmap.Decode(templatePath);

            canvas.DrawBitmap(
                templateBitmap,
                new SKRect(0, 0, width, height)
            );
        }
        else
        {
            canvas.Clear(SKColors.White);
        }

        // 🔤 2. CONFIGURA TEXTO
        using var textPaint = new SKPaint
        {
            Color = SKColors.White,
            TextSize = 40,
            IsAntialias = true,
            TextAlign = SKTextAlign.Center,
            Style = SKPaintStyle.Fill,
            Typeface = SKTypeface.FromFile(fontPath)
        };

        // 🔥 AUTO RESIZE (seguro)
        var maxTextWidth = 280;

        while (textPaint.MeasureText(renderData.BadgeName) > maxTextWidth)
        {
            textPaint.TextSize -= 2;
        }

        // 📍 POSIÇÃO (ajustada ao template)
        float x = width / 2;
        float yBase = height * 0.72f; // posição inferior (ajuste fino)

        var bounds = new SKRect();
        textPaint.MeasureText(renderData.BadgeName, ref bounds);

        float y = yBase - bounds.MidY;

        // ✍️ 3. DESENHA TEXTO
        canvas.DrawText(renderData.BadgeName, x, y, textPaint);

        // 💾 4. EXPORTA
        using var image = surface.Snapshot();
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);

        var fileName = $"{Guid.NewGuid()}.png";
        var path = Path.Combine("wwwroot", "badges", fileName);

        Directory.CreateDirectory(Path.GetDirectoryName(path)!);

        await File.WriteAllBytesAsync(path, data.ToArray());

        return $"/badges/{fileName}";
    }
}