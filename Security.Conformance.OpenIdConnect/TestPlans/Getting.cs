
using static System.Text.Json.JsonSerializer;

namespace Security.Conformance.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  internal static Task<TestPlans> GetTestPlansForCurrentUserAsync(OpenIdApiClient apiClient, PaginationRequest page, bool @public = false, CancellationToken? cancellationToken = default) =>
    GetTestPlansForCurrentUserAsync<TestPlans>(apiClient, page, @public, cancellationToken);

  internal static async Task<T> GetTestPlansForCurrentUserAsync<T>(OpenIdApiClient apiClient, PaginationRequest page, bool @public = false, CancellationToken? cancellationToken = default)
  {
    var response = await apiClient.GetTestPlansForCurrentUserAsync(@public, page, cancellationToken ?? CancellationToken.None);
    return Deserialize<T>(response.ToString()!, OpenIdApiClient.JsonSerializerOptions)!;
  }
}