
using System.Net.Http;

namespace Security.Conformance.OpenIdConnect;

partial class OpenIdConnectTests
{
  [TestMethod]
  public async Task Oidcc_client_test()
  {
    var httpClient = CreateOpenIdHttpClient();
    var apiClient = new OpenIdApiClient(httpClient);

    var testPlansPage = new PaginationRequest() { Length = 10, Start = 0 };
    var testPlans = await apiClient.GetTestPlansForCurrentUserAsync<TestPlans>(testPlansPage);

    await Task.CompletedTask;
  }

  static HttpClient CreateOpenIdHttpClient()
  {
    const string apiScheme = "Bearer";
    var apiToken = Environment.GetEnvironmentVariable("OPENID_TOKEN")!;

    var httpClient = CreateHttpClient();
    SetHttpClientAuthorizationHeader(httpClient, apiScheme, apiToken);
    return httpClient;
  }
}