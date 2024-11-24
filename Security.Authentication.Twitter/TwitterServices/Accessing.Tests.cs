
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using static Security.Authentication.Twitter.TwitterFuncs;
using static Security.Testing.Funcs;
#nullable disable

namespace Security.Authentication.Twitter;

partial class TwitterTests
{
  [TestMethod]
  public async Task Optional_query_params__access_user_informations__token_endpoint_receive_optional_params() {
    using var httpClient = CreateHttpClient("http://oauth", "/userinfo",(request) => JsonContent.Create(new {query = request.RequestUri}));
    var authOptions = CreateTwitterOptions("", "secret") with { UserInfoEndpoint = "http://oauth/userinfo", UserFields = new [] { "field1" }, ClaimMappers = [new JsonKeyClaimMapper("query_type", "query")] };

    var (claims, _) = await AccessTwitterUserInfo(string.Empty, authOptions, httpClient);
    StringAssert.Contains(GetSecurityClaim(claims, "query_type")?.Value, "user.fields=field1", StringComparison.Ordinal);
  }

  static Claim GetSecurityClaim(IEnumerable<Claim> claims, string claimType) => claims.FirstOrDefault(claim => claim.Type == claimType);
}
