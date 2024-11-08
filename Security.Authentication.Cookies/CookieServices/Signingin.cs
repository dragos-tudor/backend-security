
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
    AuthenticationCookieOptions authOptions,
    ICookieManager cookieManager,
    TicketDataFormat ticketDataFormat,
    ITicketStore ticketStore,
    DateTimeOffset currentUtc,
    ILogger logger)
  {
    SetAuthenticationPropertiesIssued(authProperties, currentUtc);
    SetAuthenticationPropertiesExpires(authProperties, currentUtc, authOptions.ExpireTimeSpan);

    var cookieName = GetCookieName(authOptions);
    var cookieOptions = BuildCookieOptions(authProperties, context);

    var authTicket = CreateAuthenticationTicket(principal, authProperties, authOptions.SchemeName);
    var cookieTicket = IsSessionBasedCookie(ticketStore)?
      CreateSessionIdTicket(
        ExtractSessionTicketId(context, cookieManager, cookieName, ticketDataFormat) switch {
          null => await SetSessionTicket(ticketStore, authTicket, context.RequestAborted),
          var ticketId => await RenewSessionTicket(ticketStore, authTicket, ticketId, context.RequestAborted)
        },
        authOptions.SchemeName):
      authTicket;

    var protectedTicket = ProtectAuthenticationTicket(cookieTicket, ticketDataFormat);
    AppendCookie(context, cookieManager, cookieName, protectedTicket, cookieOptions);
    ResetResponseCacheHeaders(context.Response);

    var redirectUri = GetRedirectUriOrQueryReturnUrl(context, authProperties, authOptions);
    if (ExistsUri(redirectUri)) SetResponseRedirect(context.Response, redirectUri!);

    LogSignedInCookie(logger, authOptions.SchemeName, GetPrincipalNameId(principal)!, context.TraceIdentifier);
    return authTicket;
  }

  public static ValueTask<AuthenticationTicket> SignInCookie (
    HttpContext context,
    ClaimsPrincipal principal,
    AuthenticationProperties? authProperties = default) =>
      SignInCookie(
        context,
        principal,
        authProperties ?? CreateCookieAuthenticationProperties(),
        ResolveRequiredService<AuthenticationCookieOptions>(context),
        ResolveRequiredService<ICookieManager>(context),
        ResolveRequiredService<TicketDataFormat>(context),
        ResolveRequiredService<ITicketStore>(context),
        ResolveRequiredService<TimeProvider>(context).GetUtcNow(),
        ResolveCookiesLogger(context));
}