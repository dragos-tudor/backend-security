
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using static Security.Testing.Funcs;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Security.Authentication.Cookies;

partial class CookiesTests
{
  [TestMethod]
  public async Task Authenticated_user_with_cookie__authenticate__authenticated_user()
  {
    using var server = CreateHttpServer(services => services.AddCookiesServices(CreateAuthenticationCookieOptions()));
    server.UseAuthentication(AuthenticateCookie);
    server.MapPost("/account/signin",(HttpContext context) => SignInCookie(context, CreateNamedClaimsPrincipal("user")).ToString());
    server.MapGet("/resource",(HttpContext context) => GetPrincipalName(context.User) );
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var signinResponse = await client.PostAsync("/account/signin");
    using var response = await client.GetAsync("/resource", GetRequestMessageCookieHeader(signinResponse));

    Assert.AreEqual("user", await ReadResponseMessageContent(response));
  }

  [TestMethod]
  public async Task Authenticated_user_with_session_based_authentication_cookie__authenticate__authenticated_user()
  {
    using var server = CreateHttpServer(services => services.AddCookiesServices(CreateAuthenticationCookieOptions(), new FakeTicketStore()));
    server.UseAuthentication(AuthenticateCookie);
    server.MapPost("/account/signin",(HttpContext context) => SignInCookie(context, CreateNamedClaimsPrincipal("user")).ToString());
    server.MapGet("/resource",(HttpContext context) => GetPrincipalName(context.User) );
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var signinResponse = await client.PostAsync("/account/signin");
    using var response = await client.GetAsync("/resource", GetRequestMessageCookieHeader(signinResponse));

    Assert.AreEqual("user", await ReadResponseMessageContent(response));
  }

  [TestMethod]
  public async Task Authenticated_user_with_cookie__authenticate__authenticated_user_microsoft()
  {
    using var server = CreateHttpServer(services => services.AddAuthentication().AddCookie());
    server.UseAuthentication();
    server.MapPost("/account/signin",(HttpContext context) => context.SignInAsync(CreatePrincipal("Cookies", new [] { CreateNameClaim("user") } )) );
    server.MapGet("/resource",(HttpContext context) => GetPrincipalName(context.User) );
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var signinResponse = await client.PostAsync("/account/signin", default);
    using var response = await client.GetAsync("/resource", GetRequestMessageCookieHeader(signinResponse));

    Assert.AreEqual("user", await ReadResponseMessageContent(response));
  }

  [TestMethod]
  public async Task Authenticated_user_by_some_api__access_other_api_resource__authenticated_user()
  {
    using var identityServer = CreateHttpServer(services => services.AddCookiesServices());
    identityServer.MapPost("/account/signin",(HttpContext context) => SignInCookie(context, CreateNamedClaimsPrincipal("user")).ToString() );
    await identityServer.StartAsync();

    using var apiServer = CreateHttpServer(services => services.AddCookiesServices());
    apiServer.UseAuthentication(AuthenticateCookie);
    apiServer.MapGet("/resource",(HttpContext context) => GetPrincipalName(context.User) );
    await apiServer.StartAsync();

    using var identityClient = identityServer.GetTestClient();
    using var signinResponse = await identityClient.PostAsync("/account/signin");

    using var apiClient = apiServer.GetTestClient();
    using var apiResponse = await apiClient.GetAsync("/resource", GetRequestMessageCookieHeader(signinResponse));

    Assert.AreEqual("user", await ReadResponseMessageContent(apiResponse));
  }

  [TestMethod]
  public async Task Authenticated_user_by_some_api__access_other_api_resource__authenticated_user_microsoft()
  {
    using var identityServer = CreateHttpServer(services => services.AddAuthentication().AddCookie());
    identityServer.MapPost("/account/signin",(HttpContext context) => context.SignInAsync(CreatePrincipal("Cookies", new [] { CreateNameClaim("user") })) );
    await identityServer.StartAsync();

    using var apiServer = CreateHttpServer(services => services.AddAuthentication().AddCookie());
    apiServer.UseAuthentication();
    apiServer.MapGet("/resource",(HttpContext context) => GetPrincipalName(context.User));
    await apiServer.StartAsync();

    using var identityClient = identityServer.GetTestClient();
    using var signinResponse = await identityClient.PostAsync("/account/signin", default);

    using var apiClient = apiServer.GetTestClient();
    using var apiResponse = await apiClient.GetAsync("/resource", GetRequestMessageCookieHeader(signinResponse));

    Assert.AreEqual("user", await ReadResponseMessageContent(apiResponse));
  }

  [TestMethod]
  public async Task Authenticated_user_with_expired_authentication_cookie__authenticate__unauthenticated_user()
  {
    var expireCookieTicket = TimeSpan.FromMinutes(10);
    using var server = CreateHttpServer(services => services.AddCookiesServices((CreateAuthenticationCookieOptions()) with { ExpireTimeSpan = expireCookieTicket }));
    server.UseAuthentication(AuthenticateCookie);
    server.MapPost("/account/signin",(HttpContext context) => SignInCookie(context, CreateNamedClaimsPrincipal("user")).ToString());
    server.MapGet("/resource",(HttpContext context) => GetPrincipalIdentity(context.User)!.IsAuthenticated ? "auth" : "unauth");
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var signinResponse = await client.PostAsync("/account/signin");
    ResolveFakeTimeProvider(server.Services).Advance(TimeSpan.FromMinutes(11));
    using var response = await client.GetAsync("/resource", GetRequestMessageCookieHeader(signinResponse));

    Assert.AreEqual("unauth", await ReadResponseMessageContent(response));
  }

  [TestMethod]
  public async Task Authenticated_user_with_expired_session_based_authentication_cookie__authenticate__unauthenticated_user()
  {
    var expireCookieTicket = TimeSpan.FromMinutes(10);
    using var server = CreateHttpServer(services => services.AddCookiesServices(
      CreateAuthenticationCookieOptions() with { ExpireTimeSpan = expireCookieTicket },
      new FakeTicketStore()));
    server.UseAuthentication(AuthenticateCookie);
    server.MapPost("/account/signin",(HttpContext context) => SignInCookie(context, CreateNamedClaimsPrincipal("user")).ToString());
    server.MapGet("/resource",(HttpContext context) => GetPrincipalIdentity(context.User)!.IsAuthenticated ? "auth" : "unauth");
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var signinResponse = await client.PostAsync("/account/signin");
    ResolveFakeTimeProvider(server.Services).Advance(TimeSpan.FromMinutes(11));
    using var response = await client.GetAsync("/resource", GetRequestMessageCookieHeader(signinResponse));

    Assert.AreEqual("unauth", await ReadResponseMessageContent(response));
  }

  [TestMethod]
  public async Task Authenticated_user_with_expired_session_based_authentication_cookie__authenticate__expired_cookie_and_removed_session_ticket_from_store()
  {
    var ticketStore = new FakeTicketStore();
    var expireCookieTicket = TimeSpan.FromMinutes(10);
    using var server = CreateHttpServer(services => services.AddCookiesServices(
      CreateAuthenticationCookieOptions() with { ExpireTimeSpan = expireCookieTicket },
      ticketStore));
    server.UseAuthentication(AuthenticateCookie);
    server.MapPost("/account/signin",(HttpContext context) => SignInCookie(context, CreateNamedClaimsPrincipal("user")).ToString());
    server.MapGet("/resource",(HttpContext context) => GetPrincipalIdentity(context.User)!.IsAuthenticated ? "auth" : "unauth");
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var signinResponse = await client.PostAsync("/account/signin");
    ResolveFakeTimeProvider(server.Services).Advance(TimeSpan.FromMinutes(11));
    var ticketId = GetSessionBasedCookieTicketId(signinResponse, server.Services);
    using var response = await client.GetAsync("/resource", GetRequestMessageCookieHeader(signinResponse));

    Assert.IsTrue(response.IsSuccessStatusCode);
    StringAssert.Contains(GetResponseMessageCookie(response), ".AspNetCore.Cookies=;", StringComparison.Ordinal);
    StringAssert.Contains(GetResponseMessageCookie(response), "expires=Thu, 01 Jan 1970", StringComparison.Ordinal);
    Assert.IsNull(await ticketStore.GetTicket(ticketId!));
  }

  [TestMethod]
  public async Task Authenticated_user_with_renewable_session_based_authentication_cookie__authenticate__authenticated_user()
  {
    var expireCookieTicket = TimeSpan.FromMinutes(10);
    using var server = CreateHttpServer(services => services.AddCookiesServices(
      CreateAuthenticationCookieOptions() with { ExpireTimeSpan = expireCookieTicket },
      new FakeTicketStore()));
    server.UseAuthentication(AuthenticateCookie);
    server.MapPost("/account/signin",(HttpContext context) => SignInCookie(context, CreateNamedClaimsPrincipal("user")).ToString());
    server.MapGet("/resource",(HttpContext context) => GetPrincipalIdentity(context.User)!.IsAuthenticated ? "auth" : "unauth");
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var signinResponse = await client.PostAsync("/account/signin");
    ResolveFakeTimeProvider(server.Services).Advance(TimeSpan.FromMinutes(7));
    using var response = await client.GetAsync("/resource", GetRequestMessageCookieHeader(signinResponse));

    Assert.AreEqual("auth", await ReadResponseMessageContent(response));
  }

  [TestMethod]
  public async Task Authenticated_user_with_renewable_session_based_authentication_cookie__authenticate__renewed_authentication_cookie()
  {
    var expireCookieTicket = TimeSpan.FromMinutes(10);
    using var server = CreateHttpServer(services => services.AddCookiesServices(
      CreateAuthenticationCookieOptions() with { ExpireTimeSpan = expireCookieTicket },
      new FakeTicketStore()));
    server.UseAuthentication(AuthenticateCookie);
    server.MapPost("/account/signin",(HttpContext context) => SignInCookie(context, CreateNamedClaimsPrincipal("user")).ToString());
    server.MapGet("/resource",(HttpContext context) => GetPrincipalIdentity(context.User)!.IsAuthenticated ? "auth" : "unauth");
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var signinResponse = await client.PostAsync("/account/signin");
    var initialCookie = GetResponseMessageCookie(signinResponse);
    ResolveFakeTimeProvider(server.Services).Advance(TimeSpan.FromMinutes(7));
    using var response = await client.GetAsync("/resource", GetRequestMessageCookieHeader(signinResponse));
    var renewedCookie = GetResponseMessageCookie(response);

    Assert.AreNotEqual(initialCookie, renewedCookie);
  }

  [TestMethod]
  public async Task Authenticated_user_with_expired_authentication_cookie__authenticate__unauthenticated_user_mictrosoft()
  {
    var expireCookieTicket = TimeSpan.FromMinutes(10);
    using var server = CreateHttpServer(services => services.AddAuthentication().AddCookie(o => o.ExpireTimeSpan = expireCookieTicket));
    server.UseAuthentication();
    server.MapPost("/account/signin",(HttpContext context) => context.SignInAsync(CreatePrincipal("Cookies", new [] { CreateNameClaim("user") } )) );
    server.MapGet("/resource",(HttpContext context) => GetPrincipalIdentity(context.User)!.IsAuthenticated ? "auth" : "unauth");
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var signinResponse = await client.PostAsync("/account/signin", default);
    ResolveFakeTimeProvider(server.Services).Advance(TimeSpan.FromMinutes(11));
    using var response = await client.GetAsync("/resource", GetRequestMessageCookieHeader(signinResponse));

    Assert.AreEqual("unauth", await ReadResponseMessageContent(response));
  }

  [TestMethod]
  public async Task Authenticated_user_by_identity_api__interop_authenticate__authenticated_user()
  {
    using var identityServer = CreateHttpServer(services => services.AddCookiesServices());
    identityServer.MapPost("/account/signin",(HttpContext context) => SignInCookie(context, CreateNamedClaimsPrincipal("user", "Cookies")).ToString() );
    await identityServer.StartAsync();

    using var apiServer = CreateHttpServer(services => services.AddAuthentication().AddCookie());
    apiServer.UseAuthentication();
    apiServer.MapGet("/resource",(HttpContext context) => GetPrincipalName(context.User));
    await apiServer.StartAsync();

    using var identityClient = identityServer.GetTestClient();
    using var signinResponse = await identityClient.PostAsync("/account/signin");

    using var apiClient = apiServer.GetTestClient();
    using var apiResponse = await apiClient.GetAsync("/resource", GetRequestMessageCookieHeader(signinResponse));

    Assert.AreEqual("user", await ReadResponseMessageContent(apiResponse));
  }

  static ClaimsPrincipal CreateNamedClaimsPrincipal(string name, string schemeName = CookieAuthenticationDefaults.AuthenticationScheme) =>
    CreatePrincipal(schemeName, new [] { CreateNameClaim(name) });

}