
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static async ValueTask<string?> SignOutCookie (
    HttpContext context,
    AuthenticationProperties authProperties,
    AuthenticationCookieOptions authOptions,
    ICookieManager cookieManager,
    TicketDataFormat ticketDataFormat,
    ITicketStore ticketStore,
    ILogger logger)
  {
    var cookieName = GetCookieName(authOptions);
    var cookieOptions = BuildCookieOptions(authProperties, context);

    if (IsSessionBasedCookie(ticketStore) &&
        ExtractSessionTicketId(context, cookieManager, cookieName, ticketDataFormat) is string ticketId)
      await RemoveSessionTicket(ticketStore, ticketId);

    DeleteCookie(context, cookieManager, cookieName, cookieOptions);
    ResetResponseCacheHeaders(context.Response);

    var redirectUri = GetRedirectUriOrQueryReturnUrl(context, authProperties, authOptions);
    if (ExistsUri(redirectUri)) SetResponseRedirect(context.Response, redirectUri!);

    LogSignedOutCookie(logger, authOptions.SchemeName, context.TraceIdentifier);
    return GetResponseLocation(context.Response);
  }

  public static ValueTask<string?> SignOutCookie (
    HttpContext context,
    AuthenticationProperties? authProperties = default) =>
      SignOutCookie(
        context,
        authProperties ?? CreateCookieAuthenticationProperties(),
        ResolveRequiredService<AuthenticationCookieOptions>(context),
        ResolveRequiredService<ICookieManager>(context),
        ResolveRequiredService<TicketDataFormat>(context),
        ResolveRequiredService<ITicketStore>(context),
        ResolveCookiesLogger(context));
}