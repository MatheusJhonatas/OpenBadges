namespace BadgeCatalog.Domain.ValueObjects;
public sealed class BadgeImage
{
    public string Url { get; }

    private BadgeImage() { } // For EF Core

    public BadgeImage(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("URL cannot be empty.", nameof(url));
        Url = url;
    }
}