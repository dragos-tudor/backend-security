using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using static Security.Testing.Funcs;

namespace Security.Authentication.BearerToken;

partial class BearerTokenTests
{
  [TestMethod]
  public void Authentication_request_with_bearer_token__authenticate__authenticated_user()
  {
    var authProps = new AuthenticationProperties(){ ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(1) };
    var ticket = new AuthenticationTicket(CreateNamedClaimsPrincipal("user"), authProps, BearerTokenDefaults.AuthenticationScheme);
    var bearerTokenProtector = CreateBearerTokenDataFormat(new EphemeralDataProtectionProvider().CreateProtector(""));
    var context = new DefaultHttpContext();
    context.Request.Headers.Authorization = BearerName + bearerTokenProtector.Protect(ticket);

    var authResult = AuthenticateBearerToken(context, DateTime.UtcNow, bearerTokenProtector);
    Assert.AreEqual("user", GetPrincipalName(authResult.Principal));
  }

  [TestMethod]
  public async Task Authenticated_user_with_bearer_token__authenticate__authenticated_user()
  {
    using var server = CreateHttpServer(services => services.AddBearerTokenServices());
    server.UseAuthentication(AuthenticateBearerToken);
    server.MapPost("/account/signin", (HttpContext context) => SignInBearerToken(context, CreateNamedClaimsPrincipal("user")));
    server.MapGet("/resource", (HttpContext context) => GetPrincipalName(context.User) ?? "unauthenticated" );
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var signinResponse = await client.PostAsync("/account/signin");
    var content = await ReadResponseMessageContent(signinResponse);

    var tokenResponse = GetAccessTokenResponse(content, server.Services);
    using var response = await client.GetAsync("/resource", (HeaderNames.Authorization, BearerName + tokenResponse!.AccessToken));

    Assert.AreEqual("user", await ReadResponseMessageContent(response));
  }

  [TestMethod]
  public void Authentication_request_without_authorization_header__authenticate__no_authentication_result()
  {
    var context = new DefaultHttpContext();
    var authResult = AuthenticateBearerToken(context, DateTime.UtcNow, default!);

    Assert.IsTrue(authResult.None);
  }

  [TestMethod]
  public void Authentication_request_without_bearer_token__authenticate__unprotecting_token_failure()
  {
    var bearerTokenProtector = CreateBearerTokenDataFormat(new EphemeralDataProtectionProvider().CreateProtector(""));
    var context = new DefaultHttpContext();
    context.Request.Headers.Authorization = BearerName;

    var authResult = AuthenticateBearerToken(context, DateTime.UtcNow, bearerTokenProtector);
    Assert.AreEqual(UnprotectingTokenFailed, authResult.Failure!.Message);
  }

  [TestMethod]
  public void Authentication_request_with_expired_bearer_token__authenticate__expired_token_failure()
  {
    var authProps = new AuthenticationProperties(){ ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(-1) };
    var expiredTicket = new AuthenticationTicket(new ClaimsPrincipal(), authProps, BearerTokenDefaults.AuthenticationScheme);
    var bearerTokenProtector = CreateBearerTokenDataFormat(new EphemeralDataProtectionProvider().CreateProtector(""));
    var context = new DefaultHttpContext();
    context.Request.Headers.Authorization = BearerName + bearerTokenProtector.Protect(expiredTicket);

    var authResult = AuthenticateBearerToken(context, DateTime.UtcNow, bearerTokenProtector);
    Assert.AreEqual(TokenExpired, authResult.Failure!.Message);
  }
}