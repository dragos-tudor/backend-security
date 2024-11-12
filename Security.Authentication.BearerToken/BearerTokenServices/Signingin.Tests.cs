
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using static Security.Testing.Funcs;

namespace Security.Authentication.BearerToken;

partial class BearerTokenTests {

  [TestMethod]
  public async Task Signin_request__signin__authentication_ticket()
  {
    var cookieOptions = CreateBearerTokenOptions() with { SchemeName = "BearerTokenScheme" };
    using var server = CreateHttpServer(services => services.AddBearerTokenServices(cookieOptions));
    server.UseAuthentication(AuthenticateBearerToken);
    server.MapPost("/api/account/signin",(HttpContext context) => SignInBearerToken(context, CreateNamedClaimsPrincipal("user", "BearerTokenScheme")));
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.PostAsync("/api/account/signin");

    var content = await ReadResponseMessageContent(response);
    var tokenResponse = GetAccessTokenResponse(content, server.Services);
    var tokenTicket = GetAccessTokenTicket(tokenResponse, server.Services);

    Assert.IsTrue(response.IsSuccessStatusCode);
    Assert.AreEqual("user", GetPrincipalName(tokenTicket!.Principal));
    Assert.AreEqual("BearerTokenScheme", tokenTicket!.Principal!.Identity!.AuthenticationType);
  }

  [TestMethod]
  public async Task Signin_request__signin__authentication_ticket_microsoft()
  {
    using var server = CreateHttpServer(services => services.AddDataProtection(Environment.CurrentDirectory + "/keys").AddAuthentication().AddBearerToken());
    server.MapPost("/api/account/signin",(HttpContext context) => context.SignInAsync(CreatePrincipal(BearerTokenDefaults.AuthenticationScheme, new [] { CreateNameClaim("user") })));
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.PostAsync("/api/account/signin", default);

    var content = await ReadResponseMessageContent(response);
    var tokenResponse = GetAccessTokenResponse(content, server.Services);
    var tokenTicket = CreateBearerTokenDataFormat(ResolveRequiredService<IDataProtectionProvider>(server.Services))
      .Unprotect(tokenResponse!.AccessToken);

    Assert.IsTrue(response.IsSuccessStatusCode);
    Assert.AreEqual("user", GetPrincipalName(tokenTicket!.Principal));
  }

  static ClaimsPrincipal CreateNamedClaimsPrincipal(string name, string schemeName =BearerTokenDefaults.AuthenticationScheme ) =>
    CreatePrincipal(schemeName, new [] { CreateNameClaim(name) });

  static AccessTokenResponse? GetAccessTokenResponse(
    string responseContent,
    IServiceProvider services) =>
      JsonSerializer.Deserialize(responseContent,
        GetAccessTokenResponseJsonTypeInfo(ResolveRequiredService<IOptions<JsonOptions>>(services))!);

  static AuthenticationTicket? GetAccessTokenTicket(
    AccessTokenResponse? accessTokenResponse,
    IServiceProvider services) =>
      ResolveRequiredService<BearerTokenDataFormat>(services)
        .Unprotect(accessTokenResponse!.AccessToken);
}
