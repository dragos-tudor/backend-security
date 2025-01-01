
using static System.Text.Json.JsonSerializer;

namespace Security.Conformance.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  internal static Task<TestInstanceInfo> GetTestInstanceInfoAsync(OpenIdApiClient apiClient, string testId, bool @public = false, CancellationToken? cancellationToken = default) =>
    GetTestInstanceInfoAsync<TestInstanceInfo>(apiClient, testId, @public, cancellationToken);

  internal static async Task<T> GetTestInstanceInfoAsync<T>(OpenIdApiClient apiClient, string testId, bool @public = false, CancellationToken? cancellationToken = default)
  {
    var response = await apiClient.GetTestInfoAsync(testId, @public, cancellationToken ?? CancellationToken.None);
    return Deserialize<T>(response.ToString()!, OpenIdApiClient.JsonSerializerOptions)!;
  }
}