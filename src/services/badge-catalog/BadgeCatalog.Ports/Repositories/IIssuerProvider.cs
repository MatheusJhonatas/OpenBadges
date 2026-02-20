

using BadgeCatalog.Domain.Issuer;

namespace BadgeCatalog.Ports;

public interface IIssuerProvider
{
    IssuerInfo GetIssuer();
}