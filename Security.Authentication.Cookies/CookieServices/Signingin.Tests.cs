
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using static Security.Testing.Funcs;

namespace Security.Authentication.Cookies;

partial class CookiesTests {

  [TestMethod]
  public async Task Signin_request__signin__authentication_cookie()
  {
    var cookieOptions = CreateAuthenticationCookieOptions("CookieName", "CookiesScheme");
    using var server = CreateHttpServer(services => services.AddCookiesServices(cookieOptions));
    server.UseAuthentication(AuthenticateCookie);
    server.MapPost("/api/account/signin",(HttpContext context) => SignInCookie(context, CreateNamedClaimsPrincipal("CookiesScheme", "user")).ToString());
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.PostAsync("/api/account/signin");

    Assert.IsTrue(response.IsSuccessStatusCode);
    StringAssert.Contains(GetResponseMessageCookie(response), "CookieName", StringComparison.Ordinal);
    StringAsserts.NotContains(GetResponseMessageCookie(response)!, "expires=Thu, 01 Jan 1970", StringComparison.Ordinal);
  }

  [TestMethod]
  public async Task Signin_non_persisting_cookie_request__signin__non_persisted_authentication_cookie()
  {
    var nonPersistedProps = new AuthenticationProperties(){ IsPersistent = false };
    using var server = CreateHttpServer(services => services.AddCookiesServices(CreateAuthenticationCookieOptions()));
    server.UseAuthentication(AuthenticateCookie);
    server.MapPost("/api/account/signin",(HttpContext context) => SignInCookie(context, CreateNamedClaimsPrincipal(CookieAuthenticationDefaults.AuthenticationScheme, "user"), nonPersistedProps).ToString());
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.PostAsync("/api/account/signin");

    Assert.IsTrue(response.IsSuccessStatusCode);
    StringAsserts.NotContains(GetResponseMessageCookie(response), "expires", StringComparison.Ordinal);
  }

  [TestMethod]
  public async Task Signin_session_based_request__signin__session_based_authentication_cookie()
  {
    var ticketStore = new FakeTicketStore();
    using var server = CreateHttpServer(services => services.AddCookiesServices(CreateAuthenticationCookieOptions(), ticketStore));
    server.UseAuthentication(AuthenticateCookie);
    server.MapPost("/api/account/signin",(HttpContext context) => SignInCookie(context, CreateNamedClaimsPrincipal(CookieAuthenticationDefaults.AuthenticationScheme, "user")).ToString());
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.PostAsync("/api/account/signin");
    var ticketId = GetSessionBasedCookieTicketId(response, server.Services);

    Assert.IsTrue(response.IsSuccessStatusCode);
    Assert.IsNotNull(await ticketStore.GetTicket(ticketId!));
  }

  [TestMethod]
  public async Task Signin_twice_session_based_request__signin__session_based_authentication_cookie()
  {
    var ticketStore = new FakeTicketStore();
    var cookieOptions = CreateAuthenticationCookieOptions() with { SchemeName = "CookiesScheme" };
    using var server = CreateHttpServer(services => services.AddCookiesServices(cookieOptions, ticketStore));
    server.UseAuthentication(AuthenticateCookie);
    server.MapPost("/api/account/signin",(HttpContext context) => SignInCookie(context, CreateNamedClaimsPrincipal("CookiesScheme", "user")).ToString());
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var firstResponse = await client.PostAsync("/api/account/signin");
    using var secondResponse = await client.PostAsync("/api/account/signin", GetRequestMessageCookieHeader(firstResponse));
    var ticketId = GetSessionBasedCookieTicketId(secondResponse, server.Services);

    Assert.IsTrue(secondResponse.IsSuccessStatusCode);
    Assert.IsNotNull(await ticketStore.GetTicket(ticketId!));
  }

  [TestMethod]
  public async Task Signin_request__signin__authentication_cookie_microsoft()
  {
    using var server = CreateHttpServer(services => services.AddAuthentication().AddCookie(o => o.Cookie.Name = "CookiesScheme"));
    server.MapPost("/api/account/signin",(HttpContext context) => context.SignInAsync(CreatePrincipal("CookiesScheme", new [] { CreateNameClaim("user") })));
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.PostAsync("/api/account/signin", default);

    Assert.IsTrue(response.IsSuccessStatusCode);
    StringAssert.Contains(GetResponseMessageCookie(response),"CookiesScheme", StringComparison.Ordinal);
  }

  static string? GetSessionBasedCookieTicketId(HttpResponseMessage response, IServiceProvider services)
  {
    var cookie = GetResponseMessageCookie(response);
    var cookieContent = GetRequestMessageCookieContent(cookie);
    var ticketDataFormat = ResolveRequiredService<TicketDataFormat>(services);

    return GetSessionId(ticketDataFormat.Unprotect(cookieContent)!);
  }
}
