
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using static Security.Testing.Funcs;

namespace Security.Authentication.OAuth;

partial class OAuthTests {

  [Fact]
  public async Task User_challenge_authentication__execute_authentication_flow__authentication_succedded () {
    using var authServer = CreateHttpServer();
    authServer.MapGet("/authorize", (HttpContext context) => SetResponseRedirect(context.Response, GetCallbackLocation(context.Request)) );
    authServer.MapPost("/token", (HttpContext request) => JsonSerializer.Serialize(new { access_token = "token" }));
    authServer.MapGet("/userinfo", (HttpContext request) => JsonSerializer.Serialize(new { email = "email", username = "username" }));
    await authServer.StartAsync();
    using var authClient = authServer.GetTestClient();

    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions() with { AuthorizationEndpoint = "http://oauth/authorize" };
    var secureDataFormat = CreateStateDataFormat(ResolveService<IDataProtectionProvider>(context));
    using var appServer = CreateHttpServer();
    appServer.MapGet("/challenge", (HttpContext context) => ChallengeRemoteOAuth(context, authOptions, secureDataFormat, DateTimeOffset.UtcNow));
    appServer.MapGet("/callback", async delegate (HttpContext context) { return (await AuthenticateOAuth(context, authOptions, secureDataFormat, authClient, PostAuthorize, ExchangeCodeForTokens, AccessUserInfo)).Succeeded; });
    await appServer.StartAsync();
    using var appClient = appServer.GetTestClient();

    var challengeResponse = await appClient.GetAsync("/challenge");
    Assert.Equal(HttpStatusCode.Redirect, challengeResponse.StatusCode);

    var authorizationUrl = GetResponseMessageLocation(challengeResponse);
    var authorizationResponse = await authClient.GetAsync(authorizationUrl);
    Assert.Equal(HttpStatusCode.Redirect, authorizationResponse.StatusCode);

    var authenticateUrl = GetResponseMessageLocation(authorizationResponse);
    var authenticationResponse = await appClient.GetAsync(authenticateUrl, GetRequestMessageCookieHeader(challengeResponse));
    Assert.Equal("true", await ReadResponseMessageContent(authenticationResponse));
  }



  static string GetCallbackLocation(HttpRequest request) =>
    $"{GetQueryParamValue(request, "redirect_uri")}?state={GetQueryParamValue(request, "state")}&code=abc";

  static string GetQueryParamValue (HttpRequest request, string keyName) =>
    request.Query[keyName]!;

}