
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using static Security.Authentication.Twitter.TwitterFuncs;
using static Security.Testing.Funcs;
#nullable disable

namespace Security.Authentication.Twitter;

partial class TwitterTests {

  [TestMethod]
  public async Task Optional_query_params__access_user_informations__token_endpoint_receive_optional_params () {
    using var httpClient = CreateHttpClient("http://oauth", "/userinfo", (request) => JsonContent.Create(new {query = request.RequestUri}));
    var authOptions = CreateTwitterOptions("", "secret") with { UserInformationEndpoint = "http://oauth/userinfo", UserFields = new [] { "field1" } };
    MapJsonClaim(authOptions, "query");

    var result = await AccessTwitterUserInfo(string.Empty, authOptions, httpClient);
    StringAssert.Contains(GetSecurityClaim(GetClaimsPrincipal(result), "query")?.Value, "user.fields=field1", StringComparison.Ordinal);
  }

  static Claim GetSecurityClaim(ClaimsPrincipal principal, string claimType) =>
    principal.Claims.FirstOrDefault(claim => claim.Type == claimType);

  static void MapJsonClaim(TwitterOptions twitterOptions, string claimType, string jsonKey = default) =>
    twitterOptions.ClaimActions.MapJsonKey(claimType, jsonKey ?? claimType);

}
