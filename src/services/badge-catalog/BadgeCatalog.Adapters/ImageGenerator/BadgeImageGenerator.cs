using BadgeCatalog.Ports.Repositories;
using SkiaSharp;

namespace BadgeCatalog.Adapters.ImageGenerator
{
    public class BadgeImageGenerator : IBadgeImageGenerator
    {
        public async Task<string> GenerateAsync(string badgeName)
        {
            const int width = 400;
            const int height = 400;

            using var surface = SKSurface.Create(new SKImageInfo(width, height));
            var canvas = surface.Canvas;

            // Fundo branco
            canvas.Clear(SKColors.White);
            using var circlePaint = new SKPaint
            {
                Color = SKColors.DarkBlue,
                IsAntialias = true
            };

            canvas.DrawCircle(
                width / 2,
                height / 2,
                160,
                circlePaint
            );

            // Configurar o estilo do texto
            using var textPaint = new SKPaint
            {
                Color = SKColors.White,
                TextSize = 32,
                IsAntialias = true,
                TextAlign = SKTextAlign.Center,
                Style = SKPaintStyle.Fill
            };

            var textBounds = new SKRect();
            textPaint.MeasureText(badgeName, ref textBounds);

            float x = width / 2;
            float y = height / 2 - textBounds.MidY;
            canvas.DrawText(badgeName, x, y, textPaint);

            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);

            var fileName = $"{Guid.NewGuid()}.png";
            var path = Path.Combine("wwwroot", "badges", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            await File.WriteAllBytesAsync(path, data.ToArray());

            return $"/badges/{fileName}";
        }
    }
}