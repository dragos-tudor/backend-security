
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using static Security.Authentication.Twitter.Funcs;
using static Security.Testing.Funcs;

namespace Security.Authentication.Twitter;

partial class Tests {

  [Fact]
  public async Task User_credentials__exchange_code_for_tokens__endpoint_receive_credentials () {
    using var authServer = CreateHttpServer();
    authServer.MapPost("/token", (HttpContext context) => new {token_type = context.Request.Headers.Authorization[0], access_token = string.Empty} );
    await authServer.StartAsync();
    using var authClient = authServer.GetTestClient();

    var context  = CreateHttpContext();
    var authOptions = CreateTwitterOptions(new EphemeralDataProtectionProvider()) with { ClientId = "id", ClientSecret = "secret", RemoteClient = authClient, TokenEndpoint = "http://oauth/token" };
    var authProperties = new AuthenticationProperties();
    var result = await ExchangeTwitterCodeForTokensAsync(authOptions, authProperties, string.Empty, context.RequestAborted);

    Assert.Equal("Basic " + GetTwitterCredentials("id", "secret"), result.TokenInfo!.TokenType);
  }

}
