
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Authentication.AuthenticateResult;

namespace Security.Authentication.Cookies;

partial class Funcs {

  const string TicketExpired = "Ticket expired";
  const string UnprotectTicketFailed = "Unprotect ticket failed";

  public static AuthenticateResult AuthenticateCookieCore (
    HttpContext context,
    CookieAuthenticationOptions authOptions,
    CookieBuilder cookieBuilder,
    DateTimeOffset currentUtc)
  {
    if (IsRequestLoginPath(context.Request, authOptions)) return NoResult();
    if (IsRequestLogoutPath(context.Request, authOptions)) return NoResult();
    var cookie = GetAuthenticationCookie(context, authOptions.CookieManager, GetCookieName(cookieBuilder, authOptions));
    if (cookie is null) return NoResult();

    var ticket = UnprotectAuthenticationTicket(cookie, authOptions.TicketDataFormat);
    if (ticket is null) return Fail(UnprotectTicketFailed);
    if (IsExpiredAuthenticationTicket(ticket, currentUtc)) return Fail(TicketExpired);
    if (!IsRenewableAuthenticationTicket(ticket, currentUtc, authOptions.SlidingExpiration)) return Success(ticket);

    return Success(SignInCookie(context, ticket.Principal, ticket.Properties, authOptions, cookieBuilder, currentUtc));
  }

  public static AuthenticateResult AuthenticateCookie (
    HttpContext context,
    CookieAuthenticationOptions authOptions,
    CookieBuilder cookieBuilder,
    DateTimeOffset currentUtc)
  {
    var result = AuthenticateCookieCore(context, authOptions, cookieBuilder, currentUtc);
    if(result.None) LogNotAuthenticated(Logger, authOptions.SchemeName, context.TraceIdentifier);
    if(result.Failure is not null) LogNotAuthenticatedWithFailure(Logger, authOptions.SchemeName, result.Failure.Message, context.TraceIdentifier);
    if(result.Succeeded) LogAuthenticated(Logger, authOptions.SchemeName, GetPrincipalNameId(result.Principal)!, context.TraceIdentifier);
    return result;
  }

}