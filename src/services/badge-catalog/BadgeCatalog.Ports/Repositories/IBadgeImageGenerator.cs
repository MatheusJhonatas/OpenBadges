using BadgeCatalog.Ports.Models;

namespace BadgeCatalog.Ports.Repositories;

public interface IBadgeImageGenerator
{
    Task<string> GenerateAsync(string templateId, BadgeRenderData renderData);
}