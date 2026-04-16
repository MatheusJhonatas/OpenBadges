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

            return Task.FromResult("ok");
        }
    }
}