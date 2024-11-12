
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

  public static async ValueTask<AuthenticateResult> AuthenticateCookie(
    HttpContext context,
    AuthenticationCookieOptions authOptions,
    DateTimeOffset currentUtc,
    ICookieManager cookieManager,
    TicketDataFormat ticketDataProtector,
    ITicketStore ticketStore)
  {
    var(authTicket, error) = ExtractAuthenticationTicket(context, authOptions, cookieManager, ticketDataProtector);
    if(error == NoCookie) return NoResult();
    if(error is not null) return Fail(error);

    if(IsSessionBasedTicket(ticketStore))
      return await AuthenticateSessionCookie(context, authOptions, authTicket, currentUtc, cookieManager, ticketDataProtector, ticketStore);

    var authTicketState = GetAuthenticationTicketState(authTicket, currentUtc, authOptions);
    if(authTicketState == AuthenticationTicketState.Valid) return Success(authTicket);
    if(authTicketState == AuthenticationTicketState.Expired) {
      CleanAuthenticationTicket(context, authTicket, authOptions, cookieManager);
      return Fail(TicketExpired);
    }

    var renewedAuthTicket = RenewAuthenticationTicket(authTicket, currentUtc);
    UseAuthenticationTicket(context, renewedAuthTicket, authOptions, cookieManager, ticketDataProtector);
    ResetResponseCacheHeaders(context.Response);

    return Success(renewedAuthTicket);
  }

  public static async Task<AuthenticateResult> AuthenticateCookie(HttpContext context) =>
    LogAuthentication(
      ResolveCookiesLogger(context),
      await AuthenticateCookie(
        context,
        ResolveRequiredService<AuthenticationCookieOptions>(context),
        ResolveRequiredService<TimeProvider>(context).GetUtcNow(),
        ResolveRequiredService<ICookieManager>(context),
        ResolveRequiredService<TicketDataFormat>(context),
        ResolveRequiredService<ITicketStore>(context)
      ),
      ResolveRequiredService<AuthenticationCookieOptions>(context).SchemeName,
      context.TraceIdentifier
    );
}