
using static System.Text.Json.JsonSerializer;

namespace Security.Conformance.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  internal static Task<TestInstanceInfo> CancelTestInstanceAsync(OpenIdApiClient apiClient, string testId, CancellationToken? cancellationToken = default) =>
    CancelTestInstanceAsync<TestInstanceInfo>(apiClient, testId, cancellationToken);

  internal static async Task<T> CancelTestInstanceAsync<T>(OpenIdApiClient apiClient, string testId, CancellationToken? cancellationToken = default)
  {
    var response = await apiClient.CancelTestAsync(testId, cancellationToken ?? CancellationToken.None);
    return Deserialize<T>(response.ToString()!, OpenIdApiClient.JsonSerializerOptions)!;
  }
}