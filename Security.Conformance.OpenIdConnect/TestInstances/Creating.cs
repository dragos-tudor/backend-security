
using static System.Text.Json.JsonSerializer;

namespace Security.Conformance.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  internal static Task<TestInstance> CreateTestInstanceAsync(OpenIdApiClient apiClient, string planId, string moduleName, TestModuleVariant moduleVariant, CancellationToken? cancellationToken = default) =>
    CreateTestInstanceAsync<TestInstance>(apiClient, planId, moduleName, moduleVariant, cancellationToken);

  internal static async Task<T> CreateTestInstanceAsync<T>(OpenIdApiClient apiClient, string planId, string moduleName, TestModuleVariant moduleVariant, CancellationToken? cancellationToken = default)
  {
    var planVariantParam = Serialize(moduleVariant, OpenIdApiClient.JsonSerializerOptions);
    var response = await apiClient.CreateTestAsync(planId, moduleName, planVariantParam, default, cancellationToken ?? CancellationToken.None);
    return Deserialize<T>(response.ToString()!, OpenIdApiClient.JsonSerializerOptions)!;
  }
}