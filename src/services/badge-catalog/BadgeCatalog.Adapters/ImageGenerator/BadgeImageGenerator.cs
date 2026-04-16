using BadgeCatalog.Ports.Repositories;

namespace BadgeCatalog.Adapters.ImageGenerator
{
    public class BadgeImageGenerator : IBadgeImageGenerator
    {
        public Task<string> GenerateAsync(string badgeName)
        {
            throw new NotImplementedException();
        }
    }
}