
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using static Security.Testing.Funcs;

namespace Security.Authentication.Cookies;

partial class CookiesTests
{
  [TestMethod]
  public async Task Session_ticket__authenticate_session_based_cookie__succeeded_with_session_ticket()
  {
    var authOptions = CreateAuthenticationCookieOptions();
    var ticketStore = new FakeTicketStore();
    var context = new DefaultHttpContext() { RequestServices = BuildServiceProvider(authOptions, ticketStore) };
    var ticket = CreateAuthenticationTicket(new ClaimsPrincipal(), CreateAuthProps(), authOptions.SchemeName);
    var ticketId = await ticketStore.SetTicket(ticket);
    var sessionTicketId = CreateSessionTicketId(ticketId, authOptions.SchemeName);

    var result = await AuthenticateSessionCookie(context, authOptions, sessionTicketId);
    Assert.IsNull(result.Failure);
    Assert.AreEqual(ticket, result.Ticket);
  }

  [TestMethod]
  public async Task Session_ticket__authenticate_session_based_cookie__session_ticket_kept_in_store()
  {
    var authOptions = CreateAuthenticationCookieOptions();
    var ticketStore = new FakeTicketStore();
    var context = new DefaultHttpContext() { RequestServices = BuildServiceProvider(authOptions, ticketStore) };
    var ticket = CreateAuthenticationTicket(new ClaimsPrincipal(), CreateAuthProps(), authOptions.SchemeName);
    var ticketId = await ticketStore.SetTicket(ticket);
    var sessionTicketId = CreateSessionTicketId(ticketId, authOptions.SchemeName);

    await AuthenticateSessionCookie(context, authOptions, sessionTicketId);
    Assert.IsNotNull(await ticketStore.GetTicket(ticketId));
  }

  [TestMethod]
  public async Task Expired_session_ticket__authenticate_session_based_cookie__ticket_expired_failure()
  {
    var authOptions = CreateAuthenticationCookieOptions();
    var ticketStore = new FakeTicketStore();
    var context = new DefaultHttpContext() { RequestServices = BuildServiceProvider(authOptions, ticketStore) };
    var ticket = CreateAuthenticationTicket(new ClaimsPrincipal(), new AuthenticationProperties() { ExpiresUtc = DateTime.Now.AddMinutes(-1) }, authOptions.SchemeName);
    var ticketId = await ticketStore.SetTicket(ticket);
    var sessionTicketId = CreateSessionTicketId(ticketId, authOptions.SchemeName);

    var result = await AuthenticateSessionCookie(context, authOptions, sessionTicketId);
    Assert.AreEqual(TicketExpired, result.Failure?.Message);
  }

  [TestMethod]
  public async Task Expired_session_ticket__authenticate_session_based_cookie__session_ticket_removed_from_store()
  {
    var authOptions = CreateAuthenticationCookieOptions();
    var ticketStore = new FakeTicketStore();
    var context = new DefaultHttpContext() { RequestServices = BuildServiceProvider(authOptions, ticketStore) };
    var ticket = CreateAuthenticationTicket(new ClaimsPrincipal(), new AuthenticationProperties() { ExpiresUtc = DateTime.Now.AddMinutes(-1) }, authOptions.SchemeName);
    var ticketId = await ticketStore.SetTicket(ticket);
    var sessionTicketId = CreateSessionTicketId(ticketId, authOptions.SchemeName);

    await AuthenticateSessionCookie(context, authOptions, sessionTicketId);
    Assert.IsNull(await ticketStore.GetTicket(ticketId));
  }

  [TestMethod]
  public async Task Renewable_session_ticket__authenticate_session_based_cookie__succedded_with_slid_session_ticket()
  {
    var authOptions = CreateAuthenticationCookieOptions();
    var ticketStore = new FakeTicketStore();
    var context = new DefaultHttpContext() { RequestServices = BuildServiceProvider(authOptions, ticketStore) };
    var authProps = new AuthenticationProperties() {
      IssuedUtc = DateTime.Now.AddMinutes(-1),
      ExpiresUtc = DateTime.Now.AddMinutes(1)
    };
    var ticket = CreateAuthenticationTicket(new ClaimsPrincipal(), authProps, authOptions.SchemeName);
    var ticketId = await ticketStore.SetTicket(ticket);
    var initialExpiresUtc = authProps.ExpiresUtc;
    var sessionTicketId = CreateSessionTicketId(ticketId, authOptions.SchemeName);

    var result = await AuthenticateSessionCookie(context, authOptions, sessionTicketId);
    Assert.IsNull(result.Failure);
    Assert.AreEqual(ticket, result.Ticket);
    Assert.IsTrue(initialExpiresUtc < ticket.Properties.ExpiresUtc);
  }

  [TestMethod]
  public async Task Renewable_session_ticket__authenticate_session_based_cookie__slide_session_ticket_kept_in__store()
  {
    var authOptions = CreateAuthenticationCookieOptions();
    var ticketStore = new FakeTicketStore();
    var context = new DefaultHttpContext() { RequestServices = BuildServiceProvider(authOptions, ticketStore) };
    var authProps = new AuthenticationProperties() {
      IssuedUtc = DateTime.Now.AddMinutes(-1),
      ExpiresUtc = DateTime.Now.AddMinutes(1)
    };
    var ticket = CreateAuthenticationTicket(new ClaimsPrincipal(), authProps, authOptions.SchemeName);
    var ticketId = await ticketStore.SetTicket(ticket);
    var sessionTicketId = CreateSessionTicketId(ticketId, authOptions.SchemeName);

    await AuthenticateSessionCookie(context, authOptions, sessionTicketId);
    Assert.IsNotNull(await ticketStore.GetTicket(ticketId));
  }

  [TestMethod]
  public async Task No_session_ticket__authenticate_session_based_cookie__missing_session_ticket_failure()
  {
    var authOptions = CreateAuthenticationCookieOptions();
    var context = new DefaultHttpContext();
    var sessionTicketId = CreateSessionTicketId("", authOptions.SchemeName);

    var result = await AuthenticateSessionCookie(context, authOptions, sessionTicketId, DateTime.UtcNow, default!, default!, new FakeTicketStore());
    Assert.AreEqual(MissingSessionTicket, result.Failure!.Message);
  }

  static ServiceProvider BuildServiceProvider(AuthenticationCookieOptions authOptions, FakeTicketStore ticketStore) =>
    new ServiceCollection()
      .AddDataProtection(Environment.CurrentDirectory + "/keys")
      .AddCookiesServices(authOptions, ticketStore)
      .BuildServiceProvider();
}