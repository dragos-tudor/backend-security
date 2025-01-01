
using static System.Text.Json.JsonSerializer;

namespace Security.Conformance.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  internal static Task<TestInstanceInfo> StartTestInstanceAsync(OpenIdApiClient apiClient, string testId, CancellationToken? cancellationToken = default) =>
    StartTestInstanceAsync<TestInstanceInfo>(apiClient, testId, cancellationToken);

  internal static async Task<T> StartTestInstanceAsync<T>(OpenIdApiClient apiClient, string testId, CancellationToken? cancellationToken = default)
  {
    var response = await apiClient.StartTestAsync(testId, cancellationToken ?? CancellationToken.None);
    return Deserialize<T>(response.ToString()!, OpenIdApiClient.JsonSerializerOptions)!;
  }
}