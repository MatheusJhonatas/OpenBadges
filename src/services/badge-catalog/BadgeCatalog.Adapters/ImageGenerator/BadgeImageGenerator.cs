using BadgeCatalog.Ports.Repositories;
using BadgeCatalog.Ports.Models;
using BadgeCatalog.Adapters.Templates;
using SkiaSharp;
using Svg.Skia;
using System.Text.RegularExpressions;

namespace BadgeCatalog.Adapters.ImageGenerator;

public class BadgeImageGenerator : IBadgeImageGenerator
{
    // ─── Constantes de layout ────────────────────────────────────────────────
    private const int   CanvasSize   = 1200;
    private const float BaseSize     = 400f;
    private const float MaxTextWidth = 850f;
    private const float MinFontSize  = 40f;

    // ─── Entry point ─────────────────────────────────────────────────────────
    public async Task<byte[]> GenerateAsync(
        string templateId,
        BadgeRenderData data)
    {
        float scale    = CanvasSize / BaseSize;
        var   template = BadgeTemplateResolver.Get(templateId);

        var paths = ResolvePaths(template);

        // Surface com canal alpha para evitar fundo preto
        var imageInfo = new SKImageInfo(
            CanvasSize,
            CanvasSize,
            SKColorType.Rgba8888,
            SKAlphaType.Premul);

        using var surface = SKSurface.Create(imageInfo);
        var       canvas  = surface.Canvas;

        canvas.Clear(SKColors.White);

        await RenderSvgBackgroundAsync(canvas, paths.Template);
        RenderText(canvas, data, template, scale);

        return ExportPng(surface);
    }

    // ─── Passo 1: Caminhos ───────────────────────────────────────────────────
    private static (string Template, string Font) ResolvePaths(
        BadgeTemplate template)
    {
        var templatePath = Path.Combine(
            "wwwroot", "templates", template.BackgroundImage);

        var fontPath = Path.Combine(
            "wwwroot", "fontes", "NotoSans-Bold.ttf");

        return (templatePath, fontPath);
    }

    // ─── Passo 2: SVG ────────────────────────────────────────────────────────
    private static async Task RenderSvgBackgroundAsync(
        SKCanvas canvas,
        string   templatePath)
    {
        if (!File.Exists(templatePath))
            return;

        var rawSvg      = await File.ReadAllTextAsync(templatePath);
        var sanitizedSvg = SanitizeSvgBackground(rawSvg);

        var svg = new SKSvg();

        using var stream = new MemoryStream(
            System.Text.Encoding.UTF8.GetBytes(sanitizedSvg));

        svg.Load(stream);

        var picture = svg.Picture;
        if (picture is null) return;

        var (scaleX, scaleY, transX, transY) =
            ComputeSvgTransform(picture.CullRect);

        canvas.Save();
        canvas.Translate(transX, transY);
        canvas.Scale(scaleX, scaleY);
        canvas.DrawPicture(picture);
        canvas.Restore();
    }

    /// <summary>
    /// Remove elementos de fundo preto/opaco do SVG antes de renderizar.
    /// Cobre os casos mais comuns encontrados em SVGs de badges exportados
    /// por ferramentas como Illustrator, Figma e Inkscape.
    /// </summary>
    private static string SanitizeSvgBackground(string svgContent)
    {
        // 1. Remove <rect> que cubra toda a área com fill preto ou escuro
        svgContent = Regex.Replace(
            svgContent,
            @"<rect\b[^>]*\bfill\s*=\s*[""'](#000000|#000|black)[""'][^>]*/?>",
            string.Empty,
            RegexOptions.IgnoreCase);

        // 2. Remove <rect> com style contendo fill preto
        svgContent = Regex.Replace(
            svgContent,
            @"<rect\b[^>]*\bstyle\s*=\s*[""'][^""']*fill\s*:\s*(#000000|#000|black)[^""']*[""'][^>]*/?>",
            string.Empty,
            RegexOptions.IgnoreCase);

        // 3. Remove background-color preto no elemento <svg> raiz
        svgContent = Regex.Replace(
            svgContent,
            @"(style\s*=\s*[""'][^""']*)background(?:-color)?\s*:\s*(#000000|#000|black)\s*;?",
            "$1",
            RegexOptions.IgnoreCase);

        return svgContent;
    }

    private static (float scaleX, float scaleY, float transX, float transY)
        ComputeSvgTransform(SKRect svgBounds)
    {
        float scaleX = CanvasSize / svgBounds.Width;
        float scaleY = CanvasSize / svgBounds.Height;
        float scale  = Math.Min(scaleX, scaleY);

        float transX = (CanvasSize - svgBounds.Width  * scale) / 2f;
        float transY = (CanvasSize - svgBounds.Height * scale) / 2f;

        return (scale, scale, transX, transY);
    }

    // ─── Passo 3: Texto ──────────────────────────────────────────────────────
    private static void RenderText(
        SKCanvas     canvas,
        BadgeRenderData data,
        BadgeTemplate   template,
        float           scale)
    {
        var fontPath = Path.Combine(
            "wwwroot", "fontes", "NotoSans-Bold.ttf");

        using var paint = BuildTextPaint(data, template, scale, fontPath);

        FitTextToWidth(paint, data.BadgeName);

        var lines = WrapText(paint, data.BadgeName);

        DrawLines(canvas, paint, lines, template, scale);
    }

    private static SKPaint BuildTextPaint(
        BadgeRenderData data,
        BadgeTemplate   template,
        float           scale,
        string          fontPath)
    {
        return new SKPaint
        {
            Color     = SKColor.Parse(data.TextColor ?? template.DefaultTextColor),
            TextSize  = template.DefaultFontSize * scale,
            IsAntialias = true,
            TextAlign = SKTextAlign.Center,
            Style     = SKPaintStyle.Fill,
            Typeface  = File.Exists(fontPath)
                ? SKTypeface.FromFile(fontPath)
                : SKTypeface.Default
        };
    }

    private static void FitTextToWidth(SKPaint paint, string text)
    {
        while (paint.MeasureText(text) > MaxTextWidth
               && paint.TextSize > MinFontSize)
        {
            paint.TextSize -= 2;
        }
    }

    private static List<string> WrapText(SKPaint paint, string text)
    {
        var lines       = new List<string>();
        var currentLine = string.Empty;

        foreach (var word in text.Split(' '))
        {
            var candidate = string.IsNullOrWhiteSpace(currentLine)
                ? word
                : $"{currentLine} {word}";

            if (paint.MeasureText(candidate) > MaxTextWidth)
            {
                if (!string.IsNullOrWhiteSpace(currentLine))
                    lines.Add(currentLine);

                currentLine = word;
            }
            else
            {
                currentLine = candidate;
            }
        }

        if (!string.IsNullOrWhiteSpace(currentLine))
            lines.Add(currentLine);

        return lines;
    }

    private static void DrawLines(
        SKCanvas      canvas,
        SKPaint       paint,
        List<string>  lines,
        BadgeTemplate template,
        float         scale)
    {
        float x          = CanvasSize / 2f;
        float lineHeight = paint.TextSize + (12 * scale);
        float startY     = (template.TextYPosition * scale)
                           - ((lines.Count - 1) * lineHeight / 2f);

        for (int i = 0; i < lines.Count; i++)
        {
            var bounds = new SKRect();
            paint.MeasureText(lines[i], ref bounds);

            float lineY = startY + (i * lineHeight) - bounds.MidY;
            canvas.DrawText(lines[i], x, lineY, paint);
        }
    }

    // ─── Passo 4: Export ─────────────────────────────────────────────────────
    private static byte[] ExportPng(SKSurface surface)
    {
        using var image     = surface.Snapshot();
        using var imageData = image.Encode(SKEncodedImageFormat.Png, 100);
        return imageData.ToArray();
    }
}