
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using static Security.Authentication.Facebook.FacebookFuncs;
using static Security.Testing.Funcs;
#nullable disable

namespace Security.Authentication.Facebook;

partial class FacebookTests
{
  [TestMethod]
  public async Task Access_token__access_user_informations__token_endpoint_receive_access_token()
  {
    using var httpClient = CreateHttpClient("http://oauth", "/userinfo", (request) => JsonContent.Create(new {query = request.RequestUri}));
    var authOptions = CreateFacebookOptions("", "secret") with { UserInfoEndpoint = "http://oauth/userinfo", ClaimMappers = [new JsonKeyClaimMapper("query_type", "query")] };

    var (claims, _) = await AccessFacebookUserInfo("token", authOptions, httpClient);
    StringAssert.Contains(GetSecurityClaim(claims, "query_type")?.Value, "access_token=token", StringComparison.InvariantCulture);
  }

  [TestMethod]
  public async Task App_secret_proof__access_user_informations__token_endpoint_receive_app_secret_proof()
  {
    using var httpClient = CreateHttpClient("http://oauth", "/userinfo", (request) => JsonContent.Create(new {secret_proof = request.RequestUri}));
    var authOptions = CreateFacebookOptions("", "secret") with { UserInfoEndpoint = "http://oauth/userinfo", ClaimMappers = [new JsonKeyClaimMapper("secret_proof_type", "secret_proof")] };

    var (claims, _) = await AccessFacebookUserInfo(string.Empty, authOptions, httpClient);
    StringAssert.Contains(GetSecurityClaim(claims, "secret_proof_type")?.Value, "appsecret_proof=", StringComparison.InvariantCulture);
  }

  [TestMethod]
  public async Task Facebook_fields__access_user_informations__token_endpoint_receive_fields()
  {
    using var httpClient = CreateHttpClient("http://oauth", "/userinfo", (request) => JsonContent.Create(new {fields = request.RequestUri}));
    var authOptions = CreateFacebookOptions("", "secret") with { UserInfoEndpoint = "http://oauth/userinfo", ClaimMappers = [new JsonKeyClaimMapper("fields_type", "fields")] };

    var (claims, _) = await AccessFacebookUserInfo(string.Empty, authOptions, httpClient);
    StringAssert.Contains(GetSecurityClaim(claims, "fields_type")?.Value, "fields=name%2Cemail", StringComparison.InvariantCulture);
  }

  static Claim GetSecurityClaim(IEnumerable<Claim> claims, string claimType) => claims.FirstOrDefault(claim => claim.Type == claimType);
}
