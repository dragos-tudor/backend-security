
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

using static Security.Testing.Funcs;

namespace Security.Authentication.Cookies;

partial class CookiesTests
{
  [TestMethod]
  public async Task Signout_session_based_request__signout__expired_session_based_authentication_cookie()
  {
    var cookieOptions = CreateAuthenticationCookieOptions("CookieName", "CookiesScheme");
    var ticketStore = new FakeTicketStore();
    using var server = CreateHttpServer(services => services.AddCookiesServices(cookieOptions, ticketStore));
    server.UseAuthentication(AuthenticateCookie);
    server.MapPost("/api/account/signin", (HttpContext context) => SignInCookie(context, CreateNamedClaimsPrincipal("CookiesScheme", "user")).ToString());
    server.MapPost("/api/account/signout", async (HttpContext context) => await SignOutCookie(context));
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var signinResponse = await client.PostAsync("/api/account/signin");
    using var signoutResponse = await client.PostAsync("/api/account/signout", GetRequestMessageCookieHeader(signinResponse));

    Assert.IsTrue(signoutResponse.IsSuccessStatusCode);
    StringAssert.Contains(GetResponseMessageCookie(signoutResponse), "CookieName=;", StringComparison.Ordinal);
    StringAssert.Contains(GetResponseMessageCookie(signoutResponse), "expires=Thu, 01 Jan 1970", StringComparison.Ordinal);
  }

  [TestMethod]
  public async Task Signout_session_based_request__signout__session_ticket_removed_from_store()
  {
    var ticketStore = new FakeTicketStore();
    using var server = CreateHttpServer(services => services.AddCookiesServices(CreateAuthenticationCookieOptions(), ticketStore));
    server.UseAuthentication(AuthenticateCookie);
    server.MapPost("/api/account/signin", (HttpContext context) => SignInCookie(context, CreateNamedClaimsPrincipal(CookieAuthenticationDefaults.AuthenticationScheme, "user")).ToString());
    server.MapPost("/api/account/signout", async (HttpContext context) => await SignOutCookie(context));
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var signinResponse = await client.PostAsync("/api/account/signin");
    var ticketId = GetSessionBasedCookieTicketId(signinResponse, server.Services);
    using var signoutResponse = await client.PostAsync("/api/account/signout", GetRequestMessageCookieHeader(signinResponse));

    Assert.IsNull(await ticketStore.GetTicket(ticketId!));
  }


  [TestMethod]
  public async Task Signout_request_without_auth_cookie__signout__no_signout()
  {
    using var server = CreateHttpServer(services => services.AddCookiesServices());
    server.UseAuthentication(AuthenticateCookie);
    server.MapPost("/api/account/signout", (HttpContext context) => SignOutCookie(context));
    await server.StartAsync();

    using var client = server.GetTestClient();
    using var response = await client.PostAsync("/api/account/signout");

    Assert.IsTrue(response.IsSuccessStatusCode);
    Assert.AreEqual(await GetResponseMessageContent(response), "false");
  }

  [TestMethod]
  public async Task Signout_request__signout__expired_cookie_microsoft()
  {
    using var server = CreateHttpServer(services => services.AddAuthentication().AddCookie());
    server.UseAuthentication();
    server.MapPost("/api/account/signin", (HttpContext context) => context.SignInAsync(CreatePrincipal("Cookies", [CreateNameClaim("user")])));
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