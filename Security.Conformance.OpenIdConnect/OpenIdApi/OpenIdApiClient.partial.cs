
using System.Text.Json;

namespace Security.Conformance.OpenIdConnect;

partial class OpenIdApiClient
{
  static partial void UpdateJsonSerializerSettings(JsonSerializerOptions settings) => settings.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

  public async Task<T> GetTestPlansForCurrentUserAsync<T>(PaginationRequest page, bool isPublic = false, CancellationToken? cancellationToken = default)
  {
    var response = await GetTestPlansForCurrentUserAsync(isPublic, page, cancellationToken ?? CancellationToken.None);
    return JsonSerializer.Deserialize<T>(response?.ToString()!, _settings.Value)!;
  }
}