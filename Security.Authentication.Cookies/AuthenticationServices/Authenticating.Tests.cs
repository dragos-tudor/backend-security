
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using static Security.Testing.Funcs;

namespace Security.Authentication.Cookies;

partial class CookiesTests
{
  [Fact]
  public async Task Authenticated_user__access_protected_resource__access_allowed()
  {
    using var server = CreateHttpServer(services => services.AddCookies(CreateCookieAuthenticationOptions()));
    var authProperties = CreateAuthenticationProperties();
    server.UseAuthentication(AuthenticateCookie);
    server.MapPost("/account/login", (HttpContext context) => SignInCookie(context, CreateNamedClaimsPrincipal("Cookies", "user"), authProperties).ToString());
    server.MapGet("/resource", (HttpContext context) => GetPrincipalName(context.User) );
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var loginResponse = await client.PostAsync("/account/login");
    using var response = await client.GetAsync("/resource", GetRequestMessageCookieHeader(loginResponse));

    Assert.Equal("user", await ReadResponseMessageContent(response));
  }

  [Fact]
  public async Task Authenticated_user__access_protected_resource__access_allowed_microsoft()
  {
    using var server = CreateHttpServer(services => services.AddAuthentication().AddCookie());
    server.UseAuthentication();
    server.MapPost("/account/login", (HttpContext context) => context.SignInAsync(CreatePrincipal("Cookies", new [] { CreateNameClaim("user") } )) );
    server.MapGet("/resource", (HttpContext context) => GetPrincipalName(context.User) );
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var loginResponse = await client.PostAsync("/account/login", default);
    using var response = await client.GetAsync("/resource", GetRequestMessageCookieHeader(loginResponse));

    Assert.Equal("user", await ReadResponseMessageContent(response));
  }

  [Fact]
  public async Task Authenticated_user_by_identity_app__access_other_api_resource__user_authenticated()
  {
    using var identityServer = CreateHttpServer(services => services.AddCookies());
    var authProperties = CreateAuthenticationProperties();
    identityServer.MapPost("/account/login", (HttpContext context) => SignInCookie(context, CreateNamedClaimsPrincipal("Cookies", "user"), authProperties).ToString() );
    await identityServer.StartAsync();

    using var apiServer = CreateHttpServer(services => services.AddCookies());
    apiServer.UseAuthentication(AuthenticateCookie);
    apiServer.MapGet("/resource", (HttpContext context) => GetPrincipalName(context.User) );
    await apiServer.StartAsync();

    using var identityClient = identityServer.GetTestClient();
    using var loginResponse = await identityClient.PostAsync("/account/login");

    using var apiClient = apiServer.GetTestClient();
    using var apiResponse = await apiClient.GetAsync("/resource", GetRequestMessageCookieHeader(loginResponse));

    Assert.Equal("user", await ReadResponseMessageContent(apiResponse));
  }

  [Fact]
  public async Task Authenticated_user_by_identity_app__access_other_api_resource__user_authenticated_microsoft()
  {
    using var identityServer = CreateHttpServer(services => services.AddAuthentication().AddCookie());
    identityServer.MapPost("/account/login", (HttpContext context) => context.SignInAsync(CreatePrincipal("Cookies", new [] { CreateNameClaim("user") })) );
    await identityServer.StartAsync();

    using var apiServer = CreateHttpServer(services => services.AddAuthentication().AddCookie());
    apiServer.UseAuthentication();
    apiServer.MapGet("/resource", (HttpContext context) => GetPrincipalName(context.User));
    await apiServer.StartAsync();

    using var identityClient = identityServer.GetTestClient();
    using var loginResponse = await identityClient.PostAsync("/account/login", default);

    using var apiClient = apiServer.GetTestClient();
    using var apiResponse = await apiClient.GetAsync("/resource", GetRequestMessageCookieHeader(loginResponse));

    Assert.Equal("user", await ReadResponseMessageContent(apiResponse));
  }

  [Fact]
  public async Task Authenticated_user__expire_authentication_cookie__unauthenticated_user()
  {
    var expireCookieTicket = TimeSpan.FromMinutes(10);
    var authProperties = CreateAuthenticationProperties();
    using var server = CreateHttpServer(services => services.AddCookies((CreateCookieAuthenticationOptions()) with { ExpireTimeSpan = expireCookieTicket }));
    server.UseAuthentication(AuthenticateCookie);
    server.MapPost("/account/login", (HttpContext context) => SignInCookie(context, CreateNamedClaimsPrincipal("Cookies", "user"), authProperties).ToString());
    server.MapGet("/resource", (HttpContext context) => GetPrincipalIdentity(context.User)!.IsAuthenticated ? "auth" : "unauth");
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var loginResponse = await client.PostAsync("/account/login");
    GetFakeTimeProvider(server.Services).Advance(TimeSpan.FromMinutes(11));
    using var response = await client.GetAsync("/resource", GetRequestMessageCookieHeader(loginResponse));

    Assert.Equal("unauth", await ReadResponseMessageContent(response));
  }

  [Fact]
  public async Task Authenticated_user__expire_authentication_cookie__unauthenticated_user_mictrosoft()
  {
    var expireCookieTicket = TimeSpan.FromMinutes(10);
    using var server = CreateHttpServer(services => services.AddAuthentication().AddCookie(o => o.ExpireTimeSpan = expireCookieTicket));
    server.UseAuthentication();
    server.MapPost("/account/login", (HttpContext context) => context.SignInAsync(CreatePrincipal("Cookies", new [] { CreateNameClaim("user") } )) );
    server.MapGet("/resource", (HttpContext context) => GetPrincipalIdentity(context.User)!.IsAuthenticated ? "auth" : "unauth");
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var loginResponse = await client.PostAsync("/account/login", default);
    GetFakeTimeProvider(server.Services).Advance(TimeSpan.FromMinutes(11));
    using var response = await client.GetAsync("/resource", GetRequestMessageCookieHeader(loginResponse));

    Assert.Equal("unauth", await ReadResponseMessageContent(response));
  }

  [Fact]
  public async Task Authenticated_user_by_identity_api__interop_access_other_api__authenticated_user()
  {
    using var identityServer = CreateHttpServer(services => services.AddCookies());
    var authProperties = CreateAuthenticationProperties();
    identityServer.MapPost("/account/login", (HttpContext context) => SignInCookie(context, CreateNamedClaimsPrincipal("Cookies", "user"), authProperties).ToString() );
    await identityServer.StartAsync();

    using var apiServer = CreateHttpServer(services => services.AddAuthentication().AddCookie());
    apiServer.UseAuthentication();
    apiServer.MapGet("/resource", (HttpContext context) => GetPrincipalName(context.User));
    await apiServer.StartAsync();

    using var identityClient = identityServer.GetTestClient();
    using var loginResponse = await identityClient.PostAsync("/account/login");

    using var apiClient = apiServer.GetTestClient();
    using var apiResponse = await apiClient.GetAsync("/resource", GetRequestMessageCookieHeader(loginResponse));

    Assert.Equal("user", await ReadResponseMessageContent(apiResponse));
  }

  static AuthenticationProperties CreateAuthenticationProperties() =>
    new ();

  static ClaimsPrincipal CreateNamedClaimsPrincipal (string schemeName, string name) =>
    CreatePrincipal(schemeName, new [] { CreateNameClaim(name) });

}