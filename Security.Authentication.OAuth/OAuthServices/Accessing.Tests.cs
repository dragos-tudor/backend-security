
using System.Net.Http.Json;
using static Security.Testing.Funcs;
#nullable disable

namespace Security.Authentication.OAuth;

partial class OAuthTests
{
  [TestMethod]
  public async Task User_info_endpoint_request_with_token__access_user_informations__endpoint_receive_access_token() {
    using var httpClient = CreateHttpClient("http://oauth", "/userinfo", (request) => JsonContent.Create(new {
      scheme = request.Headers.Authorization.Scheme,
      parameter = request.Headers.Authorization.Parameter
    }));
    var oauthOptions = CreateOAuthOptions() with { ClaimMappers = [new JsonClaimMapper("schemeType", "scheme"), new JsonClaimMapper("parameterType", "parameter")] };
    var (claims, _) = await AccessUserInfo("token_abc", oauthOptions, httpClient);

    Assert.AreEqual("Bearer", claims.FirstOrDefault(c => c.Type == "schemeType")?.Value);
    Assert.AreEqual("token_abc", claims.FirstOrDefault(c => c.Type == "parameterType")?.Value);
  }

  [TestMethod]
  public async Task Authentication_options_without_claims_issuer__access_user_informations__principal_with_scheme_name_as_issuer() {
    using var endpointResponse = JsonContent.Create(new {test = "abc"});
    using var httpClient = CreateHttpClient("http://oauth", "/userinfo", endpointResponse);
    var oauthOptions = CreateOAuthOptions() with { ClaimMappers = [new JsonClaimMapper("testType", "test")] };

    var (claims, _) = await AccessUserInfo(string.Empty, oauthOptions, httpClient);
    Assert.AreEqual(oauthOptions.SchemeName, claims.FirstOrDefault(c => c.Type == "testType")?.Issuer);
  }

  [TestMethod]
  public async Task Authentication_options_with_claims_issuer__access_user_informations__principal_with_options_issuer() {
    using var endpointResponse = JsonContent.Create(new {test = "abc"});
    using var httpClient = CreateHttpClient("http://oauth", "/userinfo", endpointResponse);
    var oauthOptions = CreateOAuthOptions() with { ClaimsIssuer = "issuer", ClaimMappers = [new JsonClaimMapper("testType", "test")] };

    var (claims, _) = await AccessUserInfo(string.Empty, oauthOptions, httpClient);
    Assert.AreEqual("issuer", claims.FirstOrDefault(c => c.Type == "testType")?.Issuer);
  }

  [TestMethod]
  public async Task User_info_endpoint_response_with_generic_error__access_user_informations__client_receive_error() {
    using var endpointResponse = JsonContent.Create(new {error = "abc"});
    using var httpClient = CreateHttpClient("http://oauth", "/userinfo", endpointResponse, 400);
    var oauthOptions = CreateOAuthOptions();
    var (_, error) = await AccessUserInfo(string.Empty, oauthOptions, httpClient);

    Assert.AreEqual(error?.ErrorType, "abc");
  }


  static Claim GetPrincipalClaim(ClaimsPrincipal principal, string claimType) => principal.Claims.FirstOrDefault(claim => claim.Type == claimType);
}
