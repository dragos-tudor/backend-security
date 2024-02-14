
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using static Security.Authentication.Facebook.FacebookFuncs;
using static Security.Testing.Funcs;
#nullable disable

namespace Security.Authentication.Facebook;

partial class FacebookTests {

  [TestMethod]
  public async Task Access_token__access_user_informations__token_endpoint_receive_access_token () {
    var httpClient = CreateHttpClient("http://oauth", "/userinfo", (request) => JsonContent.Create(new {query = request.RequestUri}));
    var authOptions = CreateFacebookOptions("", "secret") with { UserInformationEndpoint = "http://oauth/userinfo" };
    MapJsonClaim(authOptions, "query");

    var result = await AccessFacebookUserInfo("token", authOptions, httpClient);
    StringAssert.Contains(GetSecurityClaim(GetClaimsPrincipal(result), "query")?.Value, "access_token=token");
  }

  [TestMethod]
  public async Task App_secret_proof__access_user_informations__token_endpoint_receive_app_secret_proof () {
    var httpClient = CreateHttpClient("http://oauth", "/userinfo", (request) => JsonContent.Create(new {secret_proof = request.RequestUri}));
    var authOptions = CreateFacebookOptions("", "secret") with { UserInformationEndpoint = "http://oauth/userinfo" };
    MapJsonClaim(authOptions, "secret_proof");

    var result = await AccessFacebookUserInfo(string.Empty, authOptions, httpClient);
    StringAssert.Contains(GetSecurityClaim(GetClaimsPrincipal(result), "secret_proof")?.Value, "appsecret_proof=");
  }

  [TestMethod]
  public async Task Facebook_fields__access_user_informations__token_endpoint_receive_fields () {
    var httpClient = CreateHttpClient("http://oauth", "/userinfo", (request) => JsonContent.Create(new {fields = request.RequestUri}));
    var authOptions = CreateFacebookOptions("", "secret") with { UserInformationEndpoint = "http://oauth/userinfo" };
    MapJsonClaim(authOptions, "fields");

    var result = await AccessFacebookUserInfo(string.Empty, authOptions, httpClient);
    StringAssert.Contains(GetSecurityClaim(GetClaimsPrincipal(result), "fields")?.Value, "fields=name,email");
  }

  static Claim GetSecurityClaim(ClaimsPrincipal principal, string claimType) =>
    principal.Claims.FirstOrDefault(claim => claim.Type == claimType);

  static void MapJsonClaim(FacebookOptions facebookOptions, string claimType, string jsonKey = default) =>
    facebookOptions.ClaimActions.MapJsonKey(claimType, jsonKey ?? claimType);

}
