using BadgeCatalog.Ports.Repositories;
using SkiaSharp;

namespace BadgeCatalog.Adapters.ImageGenerator
{
    public class BadgeImageGenerator : IBadgeImageGenerator
    {
        public Task<string> GenerateAsync(string badgeName)
        {
            const int width = 400;
            const int height = 400;

            using var surface = SKSurface.Create(new SKImageInfo(width, height));
            var canvas = surface.Canvas;

            // Fundo branco
            canvas.Clear(SKColors.White);

            // Configurar o estilo do texto
            using var textPaint = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = 32,
                IsAntialias = true,
                TextAlign = SKTextAlign.Center
            };
            // Desenhar o nome da badge no centro
            canvas.DrawText(
                badgeName, 
                width / 2, 
                height / 2 + textPaint.TextSize / 2, textPaint);

            return Task.FromResult("ok");
        }
    }
}