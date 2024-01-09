
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

  [Fact]
  public async Task Signin_request__signin__authentication_cookie()
  {
    var cookieOptions = CreateCookieAuthenticationOptions() with { SchemeName = "CookiesScheme" };
    using var server = CreateHttpServer(services => services.AddCookies(cookieOptions));
    server.UseAuthentication(AuthenticateCookie);
    server.MapPost("/api/account/signin", (HttpContext context) => SignInCookie(context, CreateNamedClaimsPrincipal("CookiesScheme", "user")).ToString());
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.PostAsync("/api/account/signin");

    Assert.True(response.IsSuccessStatusCode);
    Assert.Contains("CookiesScheme", GetResponseMessageCookie(response));
    Assert.DoesNotContain("expires=Thu, 01 Jan 1970", GetResponseMessageCookie(response));
  }

  [Fact]
  public async Task Signin_non_persisted_cookie_request__signin__non_persited_authentication_cookie()
  {
    var nonPersistedProps = new AuthenticationProperties(){ IsPersistent = false };
    using var server = CreateHttpServer(services => services.AddCookies(CreateCookieAuthenticationOptions()));
    server.UseAuthentication(AuthenticateCookie);
    server.MapPost("/api/account/signin", (HttpContext context) => SignInCookie(context, CreateNamedClaimsPrincipal(CookieAuthenticationDefaults.AuthenticationScheme, "user"), nonPersistedProps).ToString());
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.PostAsync("/api/account/signin");

    Assert.True(response.IsSuccessStatusCode);
    Assert.DoesNotContain("expires", GetResponseMessageCookie(response));
  }

  [Fact]
  public async Task Signin_request_with_return_url__signin__response_redirected_to_return_url()
  {
    var GetAuthProperties = (HttpContext context) => new AuthenticationProperties() { RedirectUri  = context.Request.Form["redirect_url"] };
    using var server = CreateHttpServer(services => services.AddCookies(CreateCookieAuthenticationOptions()));
    server.UseAuthentication(AuthenticateCookie);
    server.MapPost("/api/accounts/signin", (HttpContext context) => SignInCookie(context,
      CreateNamedClaimsPrincipal(CookieAuthenticationDefaults.AuthenticationScheme, "user"),
      GetAuthProperties(context)).ToString());
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.PostAsync("/api/accounts/signin", new FormUrlEncodedContent(new Dictionary<string, string> { { "redirect_url", "/logged-in" } }));

    Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
    Assert.Equal("/logged-in", GetResponseMessageLocation(response));
  }

  [Fact]
  public async Task Signin_session_based_request__signin__session_based_authentication_cookie()
  {
    var ticketStore = new FakeTicketStore();
    using var server = CreateHttpServer(services => services.AddCookies(CreateCookieAuthenticationOptions(), ticketStore));
    server.UseAuthentication(AuthenticateCookie);
    server.MapPost("/api/account/signin", (HttpContext context) => SignInCookie(context, CreateNamedClaimsPrincipal(CookieAuthenticationDefaults.AuthenticationScheme, "user")).ToString());
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.PostAsync("/api/account/signin");
    var ticketId = GetSessionBasedCookieTicketId(response, server.Services);

    Assert.True(response.IsSuccessStatusCode);
    Assert.NotNull(await ticketStore.GetTicket(ticketId!));
  }

  [Fact]
  public async Task Signin_twice_session_based_request__signin__session_based_authentication_cookie()
  {
    var ticketStore = new FakeTicketStore();
    var cookieOptions = CreateCookieAuthenticationOptions() with { SchemeName = "CookiesScheme" };
    using var server = CreateHttpServer(services => services.AddCookies(cookieOptions, ticketStore));
    server.UseAuthentication(AuthenticateCookie);
    server.MapPost("/api/account/signin", (HttpContext context) => SignInCookie(context, CreateNamedClaimsPrincipal("CookiesScheme", "user")).ToString());
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var firstResponse = await client.PostAsync("/api/account/signin");
    using var secondResponse = await client.PostAsync("/api/account/signin", GetRequestMessageCookieHeader(firstResponse));
    var ticketId = GetSessionBasedCookieTicketId(secondResponse, server.Services);

    Assert.True(secondResponse.IsSuccessStatusCode);
    Assert.NotNull(await ticketStore.GetTicket(ticketId!));
  }

  [Fact]
  public async Task Signin_request__signin__authentication_cookie_microsoft()
  {
    using var server = CreateHttpServer(services => services.AddAuthentication().AddCookie(o => o.Cookie.Name = "CookiesScheme"));
    server.MapPost("/api/account/signin", (HttpContext context) => context.SignInAsync(CreatePrincipal("CookiesScheme", new [] { CreateNameClaim("user") })));
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.PostAsync("/api/account/signin", default);

    Assert.True(response.IsSuccessStatusCode);
    Assert.Contains("CookiesScheme", GetResponseMessageCookie(response));
  }

  static string? GetSessionBasedCookieTicketId(HttpResponseMessage response, IServiceProvider services)
  {
    var cookie = GetResponseMessageCookie(response);
    var cookieContent = GetRequestMessageCookieContent(cookie);
    var ticketProtector = ResolveService<TicketDataFormat>(services);

    return GetSessionTicketId(ticketProtector.Unprotect(cookieContent)!.Principal);
  }
}
