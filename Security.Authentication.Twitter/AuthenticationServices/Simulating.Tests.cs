
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.DataProtection;
using static Security.Testing.Funcs;
using static Security.Authentication.Twitter.TwitterFuncs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.WebUtilities;

namespace Security.Authentication.Twitter;

partial class TwitterTests {

  [Fact]
  public async Task User_challenge_authentication__execute_twitter_authentication_flow__authentication_succedded () {
    using var authServer = CreateHttpServer();
    authServer.MapGet("/authorize", (HttpContext context) => SetResponseRedirect(context.Response, GetCallbackLocation(context.Request)) );
    authServer.MapPost("/token", (HttpContext request) => JsonSerializer.Serialize(new { access_token = "token" }));
    authServer.MapGet("/userinfo", (HttpContext request) => JsonSerializer.Serialize(new { email = "email", username = "username" }));
    await authServer.StartAsync();
    using var authClient = authServer.GetTestClient();

    var twitterOptions = CreateTwitterOptions("id", "secret") with {
      AuthorizationEndpoint = "/authorize", TokenEndpoint = "/token", UserInformationEndpoint = "/userinfo"
    };
    using var appServer = CreateHttpServer(services => services
      .AddSingleton(authClient)
      .AddSingleton<IDataProtectionProvider>(new EphemeralDataProtectionProvider())
      .AddTwitter(twitterOptions)
    );
    appServer.MapTwitter(twitterOptions, SignIn);
    await appServer.StartAsync();
    using var appClient = appServer.GetTestClient();

    var challengeResponse = await appClient.GetAsync(twitterOptions.ChallengePath + "?returnUrl=/redirect-from");
    Assert.Equal(HttpStatusCode.Redirect, challengeResponse.StatusCode);

    var authUrl = GetResponseMessageLocation(challengeResponse);
    var authResponse = await authClient.GetAsync(authUrl);
    Assert.Equal(HttpStatusCode.Redirect, authResponse.StatusCode);

    var signinUrl = GetResponseMessageLocation(authResponse);
    var signinResponse = await appClient.GetAsync(signinUrl, GetRequestMessageCookieHeader(challengeResponse));
    Assert.Equal("/redirect-from", await ReadResponseMessageContent(signinResponse));
  }


  static string GetCallbackLocation(HttpRequest request) =>
    $"{GetQueryParamValue(request, "redirect_uri")}?state={GetQueryParamValue(request, "state")}&code=abc";

  static string GetQueryParamValue (HttpRequest request, string keyName) =>
    request.Query[keyName]!;

  static AuthenticationTicket SignIn(HttpContext _, ClaimsPrincipal principal, AuthenticationProperties authProperties) =>
    new (principal, authProperties, string.Empty);

}