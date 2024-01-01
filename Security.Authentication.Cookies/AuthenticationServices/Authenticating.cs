
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Authentication.AuthenticateResult;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  const string TicketExpired = "Ticket expired";
  const string UnprotectTicketFailed = "Unprotect ticket failed";

  public static async ValueTask<AuthenticateResult> AuthenticateCookie (
    HttpContext context,
    CookieAuthenticationOptions authOptions,
    CookieBuilder cookieBuilder,
    ICookieManager cookieManager,
    ISecureDataFormat<AuthenticationTicket> ticketProtector,
    ITicketStore ticketStore,
    DateTimeOffset currentUtc)
  {
    if (IsRequestLoginPath(context.Request, authOptions)) return NoResult();
    if (IsRequestLogoutPath(context.Request, authOptions)) return NoResult();

    var cookie = GetAuthenticationCookie(context, cookieManager, GetCookieName(cookieBuilder, authOptions));
    if (cookie is null) return NoResult();

    var protectedTicket = cookie;
    if (ticketStore is not DefaultTicketStore) protectedTicket = await ticketStore.UnstoreTicket(cookie);

    var ticket = UnprotectAuthenticationTicket(protectedTicket, ticketProtector);
    if (ticket is null) return Fail(UnprotectTicketFailed);
    if (IsExpiredAuthenticationTicket(ticket, currentUtc)) return Fail(TicketExpired);

    if (!IsRenewableAuthenticationTicket(ticket, currentUtc, authOptions.SlidingExpiration)) return Success(ticket);
    return Success(await SignInCookie(context, ticket.Principal, ticket.Properties, authOptions, cookieBuilder, cookieManager, ticketProtector, ticketStore, currentUtc));
  }

  public static async ValueTask<AuthenticateResult> AuthenticateCookie (HttpContext context)
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
    if(result.None) LogNotAuthenticated(Logger, authOptions.SchemeName, context.TraceIdentifier);
    if(result.Failure is not null) LogNotAuthenticatedWithFailure(Logger, authOptions.SchemeName, result.Failure.Message, context.TraceIdentifier);
    if(result.Succeeded) LogAuthenticated(Logger, authOptions.SchemeName, GetPrincipalNameId(result.Principal)!, context.TraceIdentifier);
    return result;
  }
}