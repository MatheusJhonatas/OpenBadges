using BadgeCatalog.Domain.Issuer;

namespace BadgeCatalog.Ports.Repositories;

public interface IIssuerProvider
{
    IssuerInfo GetIssuer();
}