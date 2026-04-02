using System.Net.Http.Json;
using Issuance.Ports.Clients;
using Issuance.Ports.Dtos;

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

    public async Task<BadgeDto?> GetByIdAsync(Guid badgeClassId, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync($"/api/badges/{badgeClassId}", cancellationToken);

    if (!response.IsSuccessStatusCode)
        return null;

    var badge = await response.Content.ReadFromJsonAsync<BadgeDto>(cancellationToken: cancellationToken);

    return badge;
    }
}