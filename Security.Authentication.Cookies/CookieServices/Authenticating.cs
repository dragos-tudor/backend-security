
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Authentication.AuthenticateResult;
#nullable disable

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  internal const string TicketExpired = "Ticket expired";
  internal const string UnprotectTicketFailed = "Unprotect ticket failed";

  public static async ValueTask<AuthenticateResult> AuthenticateCookie (
    HttpContext context,
    CookieAuthenticationOptions authOptions,
    CookieBuilder cookieBuilder,
    ICookieManager cookieManager,
    TicketDataFormat ticketDataFormat,
    ITicketStore ticketStore,
    DateTimeOffset currentUtc)
  {
    var cookieName = GetCookieName(cookieBuilder, authOptions);
    var cookie = GetAuthenticationCookie(context, cookieManager, cookieName);
    if (!ExistAuthenticationCookie(cookie)) return NoResult();

    var cookieTicket = UnprotectAuthenticationTicket(cookie, ticketDataFormat);
    if (!ExistAuthenticationTicket(cookieTicket)) return Fail(UnprotectTicketFailed);

    if (IsSessionBasedCookie(ticketStore))
      return await AuthenticateSessionCookie(context, authOptions, cookieBuilder, cookieManager,
        ticketDataFormat, ticketStore, currentUtc, GetSessionTicketId(cookieTicket.Principal));

    var cookieOptions = BuildCookieOptions(cookieBuilder, cookieTicket.Properties!, context);
    var authResult = GetAuthenticationTicketState(cookieTicket, currentUtc, authOptions) switch {
      AuthenticationTicketState.ExpiredTicket => Fail(TicketExpired),
      AuthenticationTicketState.RenewableTicket => Success(RenewAuthenticationTicket(cookieTicket, currentUtc)),
      _ => Success(cookieTicket)
    };

    if (IsExpiredAuthenticationTicket(authResult)) DeleteAuthenticationCookie(context, cookieManager, cookieName, cookieOptions);
    if (IsRenewedAuthenticationTicket(authResult, currentUtc)) AppendAuthenticationCookie(context, cookieManager, cookieName,
      ProtectAuthenticationTicket(cookieTicket, ticketDataFormat), cookieOptions);

    return authResult;
  }

  public static async Task<AuthenticateResult> AuthenticateCookie (HttpContext context)
  {
    var authOptions = ResolveService<CookieAuthenticationOptions>(context);
    var authResult = await AuthenticateCookie(
      context,
      authOptions,
      ResolveService<CookieBuilder>(context),
      ResolveService<ICookieManager>(context),
      ResolveService<TicketDataFormat>(context),
      ResolveService<ITicketStore>(context),
      ResolveService<TimeProvider>(context).GetUtcNow()
    );

    LogAuthenticationResult(Logger, authResult, authOptions.SchemeName, context.TraceIdentifier);
    return authResult;
  }
}