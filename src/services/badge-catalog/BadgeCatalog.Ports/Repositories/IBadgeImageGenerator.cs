namespace BadgeCatalog.Ports.Repositories;

public interface IBadgeImageGenerator
{
    Task<string> GenerateAsync(string badgeName);
}