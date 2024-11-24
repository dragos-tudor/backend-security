
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Logging.Abstractions;
using static Security.Testing.Funcs;

namespace Security.Authentication.OAuth;

partial class OAuthTests
{
  [TestMethod]
  public async Task OAuth_authentication__execute_authentication_flow__authentication_succedded()
  {
    using var authServer = CreateHttpServer();
    authServer.MapGet("/authorize",(HttpContext context) => SetHttpResponseRedirect(context.Response, GetCallbackLocation(context.Request)) );
    authServer.MapPost("/token",(HttpContext request) => JsonSerializer.Serialize(new { access_token = "token" }));
    authServer.MapGet("/userinfo",(HttpContext request) => JsonSerializer.Serialize(new { email = "email", username = "username" }));
    await authServer.StartAsync();
    using var authClient = authServer.GetTestClient();

    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions() with { AuthorizationEndpoint = "http://oauth/authorize" };
    var authPropsProtector = CreatePropertiesDataFormat(ResolveRequiredService<IDataProtectionProvider>(context));
    using var appServer = CreateHttpServer();
    appServer.MapGet("/challenge",(HttpContext context) => ChallengeOAuth(context, CreateAuthProps(), authOptions, DateTimeOffset.UtcNow, authPropsProtector, NullLogger.Instance));
    appServer.MapGet("/callback", async delegate(HttpContext context) { return(await AuthenticateOAuth(context, authOptions, authPropsProtector, authClient, PostAuthorization, ExchangeCodeForTokens, AccessUserInfo, NullLogger.Instance)).Succeeded; });
    await appServer.StartAsync();
    using var appClient = appServer.GetTestClient();

    var challengeResponse = await appClient.GetAsync("/challenge");
    Assert.AreEqual(HttpStatusCode.Redirect, challengeResponse.StatusCode);

    var authUrl = GetResponseMessageLocation(challengeResponse);
    var authResponse = await authClient.GetAsync(authUrl);
    Assert.AreEqual(HttpStatusCode.Redirect, authResponse.StatusCode);

    var callbackUrl = GetResponseMessageLocation(authResponse);
    var callbackResponse = await appClient.GetAsync(callbackUrl, GetRequestMessageCookieHeader(challengeResponse));
    Assert.AreEqual("true", await ReadResponseMessageContent(callbackResponse));
  }


  static string GetCallbackLocation(HttpRequest request) => $"{GetQueryParamValue(request, "redirect_uri")}?state={GetQueryParamValue(request, "state")}&code=abc";

  static string GetQueryParamValue(HttpRequest request, string keyName) => request.Query[keyName]!;

}