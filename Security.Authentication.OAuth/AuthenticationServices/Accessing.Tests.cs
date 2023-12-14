
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authentication;
using static Security.Testing.Funcs;
#nullable disable

namespace Security.Authentication.OAuth;

partial class OAuthTests {

  [Fact]
  public async Task User_info_endpoint_request_with_token__access_user_informations__endpoint_receive_access_token () {
    var httpClient = CreateHttpClient("http://oauth", "/userinfo", (request) => JsonContent.Create(new {
      scheme = request.Headers.Authorization.Scheme,
      parameter = request.Headers.Authorization.Parameter
    }));
    var authOptions = CreateOAuthOptions();
    MapJsonClaim(authOptions, "scheme");
    MapJsonClaim(authOptions, "parameter");
    var result = await AccessUserInfoAsync(authOptions, "token_abc", httpClient);

    Assert.Equal("Bearer", GetSecurityClaim(result.Principal, "scheme").Value);
    Assert.Equal("token_abc", GetSecurityClaim(result.Principal, "parameter").Value);
  }

  [Fact]
  public async Task Authentication_options_without_claims_issuer__access_user_informations__principal_with_scheme_name_as_issuer () {
    var httpClient = CreateHttpClient("http://oauth", "/userinfo", JsonContent.Create(new {test = "abc"}));
    var authOptions = CreateOAuthOptions();
    MapJsonClaim(authOptions, "test");

    var result = await AccessUserInfoAsync(authOptions, string.Empty, httpClient);
    Assert.Equal(authOptions.SchemeName, GetSecurityClaim(result.Principal, "test").Issuer);
  }

  [Fact]
  public async Task Authentication_options_with_claims_issuer__access_user_informations__principal_with_options_issuer () {
    var httpClient = CreateHttpClient("http://oauth", "/userinfo", JsonContent.Create(new {test = "abc"}));
    var authOptions = CreateOAuthOptions() with { ClaimsIssuer = "issuer" };
    MapJsonClaim(authOptions, "test");

    var result = await AccessUserInfoAsync(authOptions, string.Empty, httpClient);
    Assert.Equal("issuer", GetSecurityClaim(result.Principal, "test").Issuer);
  }

  [Fact]
  public async Task User_info_endpoint_response_with_generic_error__access_user_informations__client_receive_error () {
    var httpClient = CreateHttpClient("http://oauth", "/userinfo", JsonContent.Create(new {message = "error"}), 400);
    var authOptions = CreateOAuthOptions();
    var result = await AccessUserInfoAsync(authOptions, string.Empty, httpClient);

    Assert.StartsWith("User info endpoint failure. Status: BadRequest.", result.Failure);
  }


  static Claim GetSecurityClaim(ClaimsPrincipal principal, string claimType) =>
    principal.Claims.FirstOrDefault(claim => claim.Type == claimType);

  static void MapJsonClaim(OAuthOptions authOptions, string claimType, string jsonKey = default) =>
    authOptions.ClaimActions.MapJsonKey(claimType, jsonKey ?? claimType);

}
