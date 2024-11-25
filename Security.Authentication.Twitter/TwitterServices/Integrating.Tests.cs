
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using static Security.Testing.Funcs;
using static Security.Authentication.Twitter.TwitterFuncs;

namespace Security.Authentication.Twitter;

partial class TwitterTests {

  [TestMethod]
  public async Task Twitter_authentication__execute_authentication_flow__authentication_succedded() {
    using var authServer = CreateHttpServer();
    authServer.MapGet("/authorize", (HttpContext context) => SetHttpResponseRedirect(context.Response, GetCallbackLocation(context.Request)) );
    authServer.MapPost("/token", (HttpContext request) => JsonSerializer.Serialize(new { access_token = "token" }));
    authServer.MapGet("/userinfo", (HttpContext request) => JsonSerializer.Serialize(new { email = "email", username = "username" }));
    await authServer.StartAsync();
    using var authClient = authServer.GetTestClient();

    var twitterOptions = CreateTwitterOptions("id", "secret") with {
      AuthorizationEndpoint = "/authorize", TokenEndpoint = "/token", UserInfoEndpoint = "/userinfo"
    };
    using var appServer = CreateHttpServer(services => services
      .AddSingleton(authClient)
      .AddSingleton<IDataProtectionProvider>(new EphemeralDataProtectionProvider())
      .AddTwitterServices(twitterOptions)
    );
    appServer.MapTwitter(twitterOptions, SignIn);
    await appServer.StartAsync();
    using var appClient = appServer.GetTestClient();

    var challengeResponse = await appClient.GetAsync(twitterOptions.ChallengePath + "?returnUrl=/redirect-from");
    Assert.AreEqual(HttpStatusCode.Redirect, challengeResponse.StatusCode);

    var authUrl = GetResponseMessageLocation(challengeResponse);
    var authResponse = await authClient.GetAsync(authUrl);
    Assert.AreEqual(HttpStatusCode.Redirect, authResponse.StatusCode);

    var callbackUrl = GetResponseMessageLocation(authResponse);
    var callbackResponse = await appClient.GetAsync(callbackUrl, GetRequestMessageCookieHeader(challengeResponse));
    Assert.AreEqual("/redirect-from", await ReadResponseMessageContent(callbackResponse));
  }


  static string GetCallbackLocation(HttpRequest request) => $"{GetQueryParamValue(request, "redirect_uri")}?state={GetQueryParamValue(request, "state")}&code=abc";

  static string GetQueryParamValue(HttpRequest request, string keyName) => request.Query[keyName]!;

  static ValueTask<AuthenticationTicket> SignIn(HttpContext _, ClaimsPrincipal principal, AuthenticationProperties authProps) => new(new AuthenticationTicket(principal, authProps, string.Empty));


}