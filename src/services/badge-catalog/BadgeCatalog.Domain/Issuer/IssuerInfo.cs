namespace BadgeCatalog.Domain.Issuer;

public sealed class IssuerInfo
{
    public string Name { get; }
    public string Url { get; }
    public string Email { get; }

    public IssuerInfo(string name, string url, string email)
    {
        Name = name;
        Url = url;
        Email = email;
    }
}