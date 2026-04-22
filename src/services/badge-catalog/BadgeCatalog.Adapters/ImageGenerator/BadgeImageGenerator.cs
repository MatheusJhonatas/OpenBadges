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

            // Fundo (opcional manter branco ou azul)
            canvas.Clear(SKColors.White);

            // Círculo principal
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

            // Faixa central
            using var stripePaint = new SKPaint
            {
                Color = SKColors.White,
                IsAntialias = true
            };
            // faica central
            canvas.DrawRect(
                new SKRect(
                    40,
                    height / 2 - 30,
                    width - 40,
                    height / 2 + 30
                ),
                stripePaint
            );

            var fontPath = Path.Combine("wwwroot", "fontes", "NotoSans-Bold.ttf");

            using var textPaint = new SKPaint
            {
                Color = SKColors.White,
                TextSize = 40,
                IsAntialias = true,
                TextAlign = SKTextAlign.Center,
                Style = SKPaintStyle.Fill,
                Typeface = SKTypeface.FromFile(fontPath)
            };

            while (textPaint.MeasureText(badgeName) > width - 80)
            {
                textPaint.TextSize -= 2;
            }
            float x = width / 2;
            float bottomAreaY = height / 2 + 60;

            var textBounds = new SKRect();
            textPaint.MeasureText(badgeName, ref textBounds);

            float y = bottomAreaY - textBounds.MidY;


            var logoPath = Path.Combine("wwwroot", "templates", "LogoGlobalNTTDATABlack.png");

            if (File.Exists(logoPath))
            {
                using var logoBitmap = SKBitmap.Decode(logoPath);

                var logoWidth = 120;
                var logoHeight = 60;

                var logoX = (width - logoWidth) / 2;
                var logoY = height / 2 - (logoHeight / 2);

                canvas.DrawBitmap(
                    logoBitmap,
                    new SKRect(logoX, logoY, logoX + logoWidth, logoY + logoHeight)
                );
            }
            canvas.DrawText(badgeName, x, y, textPaint);

            // Gerar imagem
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