using BadgeCatalog.Domain.Issuer;
using BadgeCatalog.Ports;

namespace BadgeCatalog.Adapters.Issuer;

public sealed class ConfigIssuerProvider : IIssuerProvider
{
    private readonly IssuerInfo _issuer;

    public ConfigIssuerProvider()
    {
        // MVP hardcoded (depois virá do appsettings)
        _issuer = new IssuerInfo(
            "OpenBadges Platform",
            "https://openbadges.local",
            "badges@openbadges.local"
        );
    }

    public IssuerInfo GetIssuer()
    {
        return _issuer;
    }
}