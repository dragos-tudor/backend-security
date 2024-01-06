
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using static Security.Testing.Funcs;

namespace Security.Authentication.Cookies;

partial class CookiesTests
{
  [Fact]
  public async Task Session_ticket__authenticate_session_based_cookie__succeeded_with_session_ticket()
  {
    var authOptions = CreateCookieAuthenticationOptions();
    var ticketStore = new FakeTicketStore();
    var context = new DefaultHttpContext() { RequestServices = BuildServiceProvider(authOptions, ticketStore) };
    var ticket = CreateAuthenticationTicket(new ClaimsPrincipal(), new AuthenticationProperties(), authOptions.SchemeName);
    var ticketId = await ticketStore.SetTicket(ticket);

    var result = await AuthenticateSessionCookie(context, authOptions, ticketId);
    Assert.True(result.Succeeded);
    Assert.Equal(ticket, result.Ticket);
  }

  [Fact]
  public async Task Session_ticket__authenticate_session_based_cookie__session_ticket_kept_in_store()
  {
    var authOptions = CreateCookieAuthenticationOptions();
    var ticketStore = new FakeTicketStore();
    var context = new DefaultHttpContext() { RequestServices = BuildServiceProvider(authOptions, ticketStore) };
    var ticket = CreateAuthenticationTicket(new ClaimsPrincipal(), new AuthenticationProperties(), authOptions.SchemeName);
    var ticketId = await ticketStore.SetTicket(ticket);

    await AuthenticateSessionCookie(context, authOptions, ticketId);
    Assert.NotNull(await ticketStore.GetTicket(ticketId));
  }

  [Fact]
  public async Task Expired_session_ticket__authenticate_session_based_cookie__ticket_expired_failure()
  {
    var authOptions = CreateCookieAuthenticationOptions();
    var ticketStore = new FakeTicketStore();
    var context = new DefaultHttpContext() { RequestServices = BuildServiceProvider(authOptions, ticketStore) };
    var ticket = CreateAuthenticationTicket(new ClaimsPrincipal(), new AuthenticationProperties() { ExpiresUtc = DateTime.Now.AddMinutes(-1) }, authOptions.SchemeName);
    var ticketId = await ticketStore.SetTicket(ticket);

    var result = await AuthenticateSessionCookie(context, authOptions, ticketId);
    Assert.Equal(TicketExpired, result.Failure!.Message);
  }

  [Fact]
  public async Task Expired_session_ticket__authenticate_session_based_cookie__session_ticket_removed_from_store()
  {
    var authOptions = CreateCookieAuthenticationOptions();
    var ticketStore = new FakeTicketStore();
    var context = new DefaultHttpContext() { RequestServices = BuildServiceProvider(authOptions, ticketStore) };
    var ticket = CreateAuthenticationTicket(new ClaimsPrincipal(), new AuthenticationProperties() { ExpiresUtc = DateTime.Now.AddMinutes(-1) }, authOptions.SchemeName);
    var ticketId = await ticketStore.SetTicket(ticket);

    await AuthenticateSessionCookie(context, authOptions, ticketId);
    Assert.Null(await ticketStore.GetTicket(ticketId));
  }

  [Fact]
  public async Task Renewable_session_ticket__authenticate_session_based_cookie__succedded_with_slid_session_ticket()
  {
    var authOptions = CreateCookieAuthenticationOptions();
    var ticketStore = new FakeTicketStore();
    var context = new DefaultHttpContext() { RequestServices = BuildServiceProvider(authOptions, ticketStore) };
    var authProperties = new AuthenticationProperties() {
      IssuedUtc = DateTime.Now.AddMinutes(-1),
      ExpiresUtc = DateTime.Now.AddMinutes(1)
    };
    var ticket = CreateAuthenticationTicket(new ClaimsPrincipal(), authProperties, authOptions.SchemeName);
    var ticketId = await ticketStore.SetTicket(ticket);
    var initialExpiresUtc = authProperties.ExpiresUtc;

    var result = await AuthenticateSessionCookie(context, authOptions, ticketId);
    Assert.True(result.Succeeded);
    Assert.Equal(ticket, result.Ticket);
    Assert.True(initialExpiresUtc < ticket.Properties.ExpiresUtc);
  }

  [Fact]
  public async Task Renewable_session_ticket__authenticate_session_based_cookie__slid_session_ticket_kept_in__store()
  {
    var authOptions = CreateCookieAuthenticationOptions();
    var ticketStore = new FakeTicketStore();
    var context = new DefaultHttpContext() { RequestServices = BuildServiceProvider(authOptions, ticketStore) };
    var authProperties = new AuthenticationProperties() {
      IssuedUtc = DateTime.Now.AddMinutes(-1),
      ExpiresUtc = DateTime.Now.AddMinutes(1)
    };
    var ticket = CreateAuthenticationTicket(new ClaimsPrincipal(), authProperties, authOptions.SchemeName);
    var ticketId = await ticketStore.SetTicket(ticket);

    await AuthenticateSessionCookie(context, authOptions, ticketId);
    Assert.NotNull(await ticketStore.GetTicket(ticketId));
  }

  [Fact]
  public async Task No_session_ticket__authenticate_session_based_cookie__missing_session_ticket_failure()
  {
    var authOptions = CreateCookieAuthenticationOptions();
    var context = new DefaultHttpContext();

    var result = await AuthenticateSessionCookie(context, authOptions,
      default!, default!, default!, new FakeTicketStore(), DateTime.UtcNow, string.Empty);
    Assert.Equal(MissingSessionTicket, result.Failure!.Message);
  }

  [Fact]
  public async Task No_session_ticket_id__authenticate_session_based_cookie__missing_session_ticket_id_failure()
  {
    var authOptions = CreateCookieAuthenticationOptions();
    var context = new DefaultHttpContext();

    var result = await AuthenticateSessionCookie(context, authOptions,
      default!, default!, default!, default!, DateTime.UtcNow, default);
    Assert.Equal(MissingSessionTicketId, result.Failure!.Message);
  }

  IServiceProvider BuildServiceProvider(CookieAuthenticationOptions authOptions, FakeTicketStore ticketStore) =>
    new ServiceCollection()
      .AddDataProtection(Environment.CurrentDirectory + "/keys")
      .AddCookies(authOptions, ticketStore)
      .BuildServiceProvider();

}