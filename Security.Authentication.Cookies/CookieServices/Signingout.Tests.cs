
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Collections.Generic;
using System.Net.Http;
using static Security.Testing.Funcs;


namespace Security.Authentication.Cookies;

partial class CookiesTests {

  [TestMethod]
  public async Task Signout_request__signout__expired_cookie()
  {
    using var server = CreateHttpServer(services => services.AddCookies() );
    server.UseAuthentication(AuthenticateCookie);
    server.MapPost("/api/account/signout", async (HttpContext context) => (await SignOutCookie(context)) ?? string.Empty);
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.PostAsync("/api/account/signout");

    Assert.IsTrue(response.IsSuccessStatusCode);
    StringAssert.Contains(GetResponseMessageCookie(response), ".AspNetCore.Cookies=;", StringComparison.Ordinal);
    StringAssert.Contains(GetResponseMessageCookie(response), "expires=Thu, 01 Jan 1970", StringComparison.Ordinal);
  }

  [TestMethod]
  public async Task Signout_session_based_request__signout__expired_session_based_authentication_cookie()
  {
    var cookieOptions = CreateCookieAuthenticationOptions() with { SchemeName = "CookiesScheme" };
    var ticketStore = new FakeTicketStore();
    using var server = CreateHttpServer(services => services.AddCookies(cookieOptions, ticketStore));
    server.UseAuthentication(AuthenticateCookie);
    server.MapPost("/api/account/signin", (HttpContext context) => SignInCookie(context, CreateNamedClaimsPrincipal("CookiesScheme", "user")).ToString());
    server.MapPost("/api/account/signout", async (HttpContext context) => (await SignOutCookie(context)) ?? string.Empty);
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var signinResponse = await client.PostAsync("/api/account/signin");
    using var signoutResponse = await client.PostAsync("/api/account/signout", GetRequestMessageCookieHeader(signinResponse));

    Assert.IsTrue(signoutResponse.IsSuccessStatusCode);
    StringAssert.Contains(GetResponseMessageCookie(signoutResponse), "CookiesScheme=;", StringComparison.Ordinal);
    StringAssert.Contains(GetResponseMessageCookie(signoutResponse), "expires=Thu, 01 Jan 1970", StringComparison.Ordinal);
  }

  [TestMethod]
  public async Task Signout_session_based_request__signout__session_ticket_removed_from_store()
  {
    var ticketStore = new FakeTicketStore();
    using var server = CreateHttpServer(services => services.AddCookies(CreateCookieAuthenticationOptions(), ticketStore));
    server.UseAuthentication(AuthenticateCookie);
    server.MapPost("/api/account/signin", (HttpContext context) => SignInCookie(context, CreateNamedClaimsPrincipal(CookieAuthenticationDefaults.AuthenticationScheme, "user")).ToString());
    server.MapPost("/api/account/signout", async (HttpContext context) => (await SignOutCookie(context)) ?? string.Empty);
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var signinResponse = await client.PostAsync("/api/account/signin");
    var ticketId = GetSessionBasedCookieTicketId(signinResponse, server.Services);
    using var signoutResponse = await client.PostAsync("/api/account/signout", GetRequestMessageCookieHeader(signinResponse));

    Assert.IsNull(await ticketStore.GetTicket(ticketId!));
  }

  [TestMethod]
  public async Task Signout_request_with_return_url__signin__response_redirected_to_return_url()
  {
    var GetAuthProperties = (HttpContext context) => new AuthenticationProperties() { RedirectUri = context.Request.Form["redirect_url"] };
    using var server = CreateHttpServer(services => services.AddCookies() );
    server.UseAuthentication(AuthenticateCookie);
    server.MapPost("/api/accounts/signout", (HttpContext context) => SignOutCookie(context, GetAuthProperties(context)));
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var form = new FormUrlEncodedContent(new Dictionary<string, string> { { "redirect_url", "/logged-out" } });
    using var response = await client.PostAsync("/api/accounts/signout", form);

    Assert.AreEqual(HttpStatusCode.Redirect, response.StatusCode);
    Assert.AreEqual("/logged-out", GetResponseMessageLocation(response));
  }

  [TestMethod]
  public async Task Signout_request__signout__expired_cookie_microsoft()
  {
    using var server = CreateHttpServer(services => services.AddAuthentication().AddCookie());
    server.UseAuthentication();
    server.MapPost("/api/account/signin", (HttpContext context) => context.SignInAsync(CreatePrincipal("Cookies", new [] { CreateNameClaim("user") })));
    server.MapPost("/api/account/signout", (HttpContext context) => context.SignOutAsync());
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var loginResponse = await client.PostAsync("/api/account/signin");
    using var response = await client.PostAsync("/api/account/signout", GetRequestMessageCookieHeader(loginResponse));

    Assert.IsTrue(response.IsSuccessStatusCode);
    StringAssert.Contains(GetResponseMessageCookie(response), ".AspNetCore.Cookies=;", StringComparison.Ordinal);
    StringAssert.Contains(GetResponseMessageCookie(response), "expires=Thu, 01 Jan 1970", StringComparison.Ordinal);
  }

}