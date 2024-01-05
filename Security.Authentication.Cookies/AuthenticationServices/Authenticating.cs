
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static async Task<AuthenticateResult> AuthenticateCookie (
    HttpContext context,
    CookieAuthenticationOptions authOptions,
    CookieBuilder cookieBuilder,
    ICookieManager cookieManager,
    ISecureDataFormat<AuthenticationTicket> ticketProtector,
    ITicketStore ticketStore,
    DateTimeOffset currentUtc)
  {
    var authResult = await AuthenticateCoreCookie(context, authOptions, cookieBuilder,
      cookieManager, ticketProtector, ticketStore, currentUtc);

    var isExpiredTicket = IsExpiredResultTicket(authResult);
    var isRenewableTicket = IsRenewableResultTicket(authResult, currentUtc);
    if (!isExpiredTicket && !isRenewableTicket) return authResult;

    var cookieName = GetCookieName(cookieBuilder, authOptions);
    var cookieOptions = BuildCookieOptions(cookieBuilder, authResult.Properties!, context);
    var cookieTicket = ExistsTicketStore(ticketStore)?
      CreateSessionIdTicket(GetSessionTicketId(context)!, authOptions.SchemeName):
      authResult.Ticket!;

    if (isExpiredTicket) DeleteAuthenticationCookie(context, cookieManager, cookieName, cookieOptions);
    if (isRenewableTicket) AppendAuthenticationCookie(context, cookieManager, cookieName,
      ProtectAuthenticationTicket(cookieTicket, ticketProtector), cookieOptions);

    var ticketId = GetSessionTicketId(context);
    if (ticketId is null) return authResult;

    if (isExpiredTicket) await RemoveSessionTicket(ticketStore, ticketId, context.RequestAborted);
    if (isRenewableTicket) await RenewSessionTicket(ticketStore, authResult.Ticket!, ticketId, context.RequestAborted);
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