
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
#pragma warning disable ASP0016

namespace Security.Authentication.BearerToken;

partial class BearerTokenTests {

  [Fact]
  public async Task Signin_request__signin__authentication_ticket()
  {
    var cookieOptions = CreateBearerTokenOptions() with { SchemeName = "BearerTokenScheme" };
    using var server = CreateHttpServer(services => services.AddBearerToken(cookieOptions));
    server.UseAuthentication(AuthenticateBearerToken);
    server.MapPost("/api/account/signin", (HttpContext context) => SignInBearerToken(context, CreateNamedClaimsPrincipal("BearerTokenScheme", "user")));
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.PostAsync("/api/account/signin");

    var content = await ReadResponseMessageContent(response);
    var tokenResponse = GetAccessTokenResponse(content, server.Services);
    var tokenTicket = GetAccessTokenTicket(tokenResponse, server.Services);

    Assert.True(response.IsSuccessStatusCode);
    Assert.Equal("user", GetPrincipalName(tokenTicket!.Principal));
    Assert.Equal("BearerTokenScheme", tokenTicket!.Principal!.Identity!.AuthenticationType);
  }

  [Fact]
  public async Task Signin_request__signin__authentication_ticket_microsoft()
  {
    using var server = CreateHttpServer(services => services.AddDataProtection(Environment.CurrentDirectory + "/keys").AddAuthentication().AddBearerToken());
    server.MapPost("/api/account/signin", (HttpContext context) => context.SignInAsync(CreatePrincipal(BearerTokenDefaults.AuthenticationScheme, new [] { CreateNameClaim("user") })));
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.PostAsync("/api/account/signin", default);

    var content = await ReadResponseMessageContent(response);
    var tokenResponse = GetAccessTokenResponse(content, server.Services);
    var tokenTicket = CreateBearerTokenTicketProtector(ResolveService<IDataProtectionProvider>(server.Services))
      .Unprotect(tokenResponse!.AccessToken);

    Assert.True(response.IsSuccessStatusCode);
    Assert.Equal("user", GetPrincipalName(tokenTicket!.Principal));
  }

  static ClaimsPrincipal CreateNamedClaimsPrincipal (string schemeName, string name) =>
    CreatePrincipal(schemeName, new [] { CreateNameClaim(name) });

  static AccessTokenResponse? GetAccessTokenResponse(
    string responseContent,
    IServiceProvider services) =>
      JsonSerializer.Deserialize(responseContent,
        GetAccessTokenResponseJsonTypeInfo(ResolveService<IOptions<JsonOptions>>(services))!);

  static AuthenticationTicket? GetAccessTokenTicket(
    AccessTokenResponse? accessTokenResponse,
    IServiceProvider services) =>
      ResolveService<BearerTokenProtector>(services)
        .Unprotect(accessTokenResponse!.AccessToken);
}
