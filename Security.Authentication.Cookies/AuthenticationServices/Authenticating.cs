
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Authentication.AuthenticateResult;

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
    ISecureDataFormat<AuthenticationTicket> ticketProtector,
    ITicketStore ticketStore,
    DateTimeOffset currentUtc)
  {
    var cookieName = GetCookieName(cookieBuilder, authOptions);
    var cookie = GetAuthenticationCookie(context, cookieManager, cookieName);
    if (cookie is null) return NoResult();

    var cookieTicket = UnprotectAuthenticationTicket(cookie, ticketProtector);
    if (cookieTicket is null) return Fail(UnprotectTicketFailed);

    if (IsSessionBasedCookie(ticketStore))
      return await AuthenticateSessionCookie(context, authOptions, cookieBuilder, cookieManager,
        ticketProtector, ticketStore, currentUtc, GetSessionTicketId(cookieTicket.Principal));

    var cookieOptions = BuildCookieOptions(cookieBuilder, cookieTicket.Properties!, context);
    var authResult = GetAuthenticationTicketState(cookieTicket, currentUtc, authOptions) switch {
      AuthenticationTicketState.ExpiredTicket => Fail(TicketExpired),
      AuthenticationTicketState.RenewableTicket => Success(RenewAuthenticationTicket(cookieTicket, currentUtc)),
      _ => Success(cookieTicket)
    };

    if (IsExpiredAuthenticationTicket(authResult)) DeleteAuthenticationCookie(context, cookieManager, cookieName, cookieOptions);
    if (IsRenewedAuthenticationTicket(authResult, currentUtc)) AppendAuthenticationCookie(context, cookieManager, cookieName,
      ProtectAuthenticationTicket(cookieTicket, ticketProtector), cookieOptions);

    return authResult;
  }

  public static async Task<AuthenticateResult> AuthenticateCookie (HttpContext context)
  {
    var authOptions = ResolveService<CookieAuthenticationOptions>(context);
    var result = await AuthenticateCookie(
      context,
      authOptions,
      ResolveService<CookieBuilder>(context),
      ResolveService<ICookieManager>(context),
      ResolveService<ISecureDataFormat<AuthenticationTicket>>(context),
      ResolveService<ITicketStore>(context),
      ResolveService<TimeProvider>(context).GetUtcNow()
    );

    var schemeName = authOptions.SchemeName;
    var traceId = context.TraceIdentifier;
    if(result.None) LogNotAuthenticated(Logger, schemeName, traceId);
    if(result.Failure is not null) LogNotAuthenticatedWithFailure(Logger, schemeName, result.Failure.Message, traceId);
    if(result.Succeeded) LogAuthenticated(Logger, schemeName, GetPrincipalNameId(result.Principal)!, traceId);
    return result;
  }
}