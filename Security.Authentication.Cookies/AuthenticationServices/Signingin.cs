
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static async ValueTask<AuthenticationTicket> SignInCookie (
    HttpContext context,
    ClaimsPrincipal principal,
    AuthenticationProperties authProperties,
    CookieAuthenticationOptions authOptions,
    CookieBuilder cookieBuilder,
    ICookieManager cookieManager,
    ISecureDataFormat<AuthenticationTicket> ticketProtector,
    ITicketStore ticketStore,
    DateTimeOffset currentUtc)
  {
    SetAuthenticationPropertiesIssued(authProperties, currentUtc);
    SetAuthenticationPropertiesExpires(authProperties, currentUtc, authOptions.ExpireTimeSpan);

    var cookieName = GetCookieName(cookieBuilder, authOptions);
    var cookieOptions = BuildCookieOptions(cookieBuilder, authProperties, context);

    var authTicket = CreateAuthenticationTicket(principal, authProperties, authOptions.SchemeName);
    var sessionTicketId = GetSessionTicketId(context);
    var cookieTicket = ExistsTicketStore(ticketStore)?
      CreateSessionIdTicket(
        sessionTicketId is null?
          await SetSessionTicket(ticketStore, authTicket, context.RequestAborted):
          await RenewSessionTicket(ticketStore, authTicket, sessionTicketId, context.RequestAborted),
        authOptions.SchemeName):
      authTicket;

    var protectedTicket = ProtectAuthenticationTicket(cookieTicket, ticketProtector);
    AppendAuthenticationCookie(context, cookieManager, cookieName, protectedTicket, cookieOptions);

    ResetResponseCacheHeaders(context.Response);
    if (ResolveRedirectUri(context, authProperties, authOptions) is string redirectUri)
      SetResponseRedirect(context.Response, redirectUri);

    LogSignedInCookie(Logger, authOptions.SchemeName, GetPrincipalNameId(principal)!, context.TraceIdentifier);
    return authTicket;
  }

  public static ValueTask<AuthenticationTicket> SignInCookie (
    HttpContext context,
    ClaimsPrincipal principal,
    AuthenticationProperties authProperties) =>
      SignInCookie(
        context,
        principal,
        authProperties,
        ResolveService<CookieAuthenticationOptions>(context),
        ResolveService<CookieBuilder>(context),
        ResolveService<ICookieManager>(context),
        ResolveService<ISecureDataFormat<AuthenticationTicket>>(context),
        ResolveService<ITicketStore>(context),
        ResolveService<TimeProvider>(context).GetUtcNow());
}