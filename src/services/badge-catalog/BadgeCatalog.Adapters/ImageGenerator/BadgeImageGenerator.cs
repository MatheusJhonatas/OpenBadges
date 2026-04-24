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
        canvas.DrawText(data.BadgeName, x, y, textPaint);

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