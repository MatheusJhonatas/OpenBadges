using BadgeCatalog.Domain.Security;

namespace BadgeCatalog.Ports.Repositories;

public interface IJwkProvider
{
    JwkKey GetCurrent();
}
