using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using static Security.Testing.Funcs;
using static Security.Authentication.Facebook.FacebookFuncs;

namespace Security.Authentication.Facebook;

partial class FacebookTests {

  [TestMethod]
  public async Task Facebook_authentication__execute_authentication_flow__authentication_succedded () {
    using var authServer = CreateHttpServer();
    authServer.MapGet("/authorize", (HttpContext context) => SetResponseRedirect(context.Response, GetCallbackLocation(context.Request)) );
    authServer.MapPost("/token", (HttpContext request) => JsonSerializer.Serialize(new { access_token = "token" }));
    authServer.MapGet("/userinfo", (HttpContext request) => JsonSerializer.Serialize(new { email = "email", username = "username" }));
    await authServer.StartAsync();
    using var authClient = authServer.GetTestClient();

    var facebookOptions = CreateFacebookOptions("id", "secret") with {
      AuthorizationEndpoint = "/authorize", TokenEndpoint = "/token", UserInformationEndpoint = "/userinfo"
    };
    using var appServer = CreateHttpServer(services => services
      .AddSingleton(authClient)
      .AddSingleton<IDataProtectionProvider>(new EphemeralDataProtectionProvider())
      .AddFacebookServices(facebookOptions)
    );
    appServer.MapFacebook(facebookOptions, SignIn);
    await appServer.StartAsync();
    using var appClient = appServer.GetTestClient();

    var challengeResponse = await appClient.GetAsync(facebookOptions.ChallengePath + "?returnUrl=/redirect-from");
    Assert.AreEqual(HttpStatusCode.Redirect, challengeResponse.StatusCode);

    var authnUrl = GetResponseMessageLocation(challengeResponse);
    var authResponse = await authClient.GetAsync(authnUrl);
    Assert.AreEqual(HttpStatusCode.Redirect, authResponse.StatusCode);

    var signinUrl = GetResponseMessageLocation(authResponse);
    var signinResponse = await appClient.GetAsync(signinUrl, GetRequestMessageCookieHeader(challengeResponse));
    Assert.AreEqual("/redirect-from", await ReadResponseMessageContent(signinResponse));
  }


  static string GetCallbackLocation(HttpRequest request) =>
    $"{GetQueryParamValue(request, "redirect_uri")}?state={GetQueryParamValue(request, "state")}&code=abc";

  static string GetQueryParamValue (HttpRequest request, string keyName) =>
    request.Query[keyName]!;

  static ValueTask<AuthenticationTicket> SignIn(HttpContext _, ClaimsPrincipal principal, AuthenticationProperties authProperties) =>
    new(new AuthenticationTicket(principal, authProperties, string.Empty));

}