
global using static Security.Testing.Funcs;
global using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;

namespace Security.Conformance.OpenIdConnect;

[TestClass]
public partial class OpenIdConnectTests
{
  static readonly HttpClient HttpClient = CreateOpenIdHttpClient();
  static readonly OpenIdApiClient ApiClient = new (HttpClient);

  static HttpClient CreateOpenIdHttpClient()
  {
    const string apiScheme = "Bearer";
    var apiToken = Environment.GetEnvironmentVariable("OPENID_TOKEN")!;

    var httpClient = CreateHttpClient();
    SetHttpClientAuthorizationHeader(httpClient, apiScheme, apiToken);
    return httpClient;
  }
}