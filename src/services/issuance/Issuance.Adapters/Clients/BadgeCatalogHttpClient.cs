using Issuance.Ports.Clients;

namespace Issuance.Adapters.Clients;

public sealed class BadgeCatalogHttpClient : IBadgeCatalogClient
{
    private readonly HttpClient _httpClient;
    public BadgeCatalogHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<bool> BadgeExistsAsync(Guid badgeClassId, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync($"/api/badges/{badgeClassId}", cancellationToken);
        return response.IsSuccessStatusCode;
    }
}