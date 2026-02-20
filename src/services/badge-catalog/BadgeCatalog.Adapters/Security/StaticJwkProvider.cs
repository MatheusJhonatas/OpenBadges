using BadgeCatalog.Domain.Security;
using BadgeCatalog.Ports.Repositories;

namespace BadgeCatalog.Adapters.Security;

public sealed class StaticJwkProvider : IJwkProvider
{
    private readonly JwkKey _key;
    public StaticJwkProvider()
    {
        // MVP chave fake (depois virá real)
        _key = new JwkKey(
            "RSA",
            "openbadges-2026",
            "sig",
            "RS256",
            "fake-modulus",
            "AQAB");
    }

    public JwkKey GetCurrent()
    {
        return _key;
    }
}