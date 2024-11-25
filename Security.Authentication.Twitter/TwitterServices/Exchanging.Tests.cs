
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using static Security.Authentication.Twitter.TwitterFuncs;
using static Security.Testing.Funcs;

namespace Security.Authentication.Twitter;

partial class TwitterTests
{
  [TestMethod]
  public async Task User_credentials__exchange_code_for_tokens__endpoint_receive_credentials() {
    using var authServer = CreateHttpServer();
    authServer.MapPost("/token", (HttpContext context) => new {token_type = context.Request.Headers.Authorization[0], access_token = "a"} );
    await authServer.StartAsync();
    using var authClient = authServer.GetTestClient();

    var authOptions = CreateTwitterOptions("id", "secret") with { TokenEndpoint = "http://oauth/token" };
    var authProps = new AuthenticationProperties();
    var (tokenInfo, _) = await ExchangeTwitterCodeForTokens(string.Empty, authProps, authOptions, authClient);

    Assert.AreEqual("Basic " + GetTwitterCredentials("id", "secret"), tokenInfo!.TokenType);
  }
}
