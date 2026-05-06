using BadgeCatalog.Ports.Models;

namespace BadgeCatalog.Ports.Repositories;

public interface IBadgeImageGenerator
{
    Task<byte[]> GenerateAsync(string templateId, BadgeRenderData renderData);
}