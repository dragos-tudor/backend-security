
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static async ValueTask<string?> SignOutCookie (
    HttpContext context,
    AuthenticationProperties authProperties,
    CookieAuthenticationOptions authOptions,
    CookieBuilder cookieBuilder,
    ICookieManager cookieManager,
    TicketDataFormat ticketDataFormat,
    ITicketStore ticketStore,
    ILogger logger)
  {
    var cookieOptions = BuildCookieOptions(cookieBuilder, authProperties, context);
    var cookieName = GetCookieName(cookieBuilder, authOptions);

    if (IsSessionBasedCookie(ticketStore) &&
        ExtractSessionTicketId(context, cookieManager, cookieName, ticketDataFormat) is string ticketId)
      await RemoveSessionTicket(ticketStore, ticketId);

    DeleteAuthenticationCookie(context, cookieManager, cookieName, cookieOptions);
    ResetResponseCacheHeaders(context.Response);
    SetResponseRedirect(context.Response, GetRedirectUriOrQueryReturnUrl(context, authProperties, authOptions));

    LogSignedOutCookie(logger, authOptions.SchemeName, context.TraceIdentifier);
    return GetResponseLocation(context.Response);
  }

  public static ValueTask<string?> SignOutCookie (
    HttpContext context,
    AuthenticationProperties? authProperties = default) =>
      SignOutCookie(
        context,
        authProperties ?? CreateCookieAuthenticationProperties(),
        ResolveRequiredService<CookieAuthenticationOptions>(context),
        ResolveRequiredService<CookieBuilder>(context),
        ResolveRequiredService<ICookieManager>(context),
        ResolveRequiredService<TicketDataFormat>(context),
        ResolveRequiredService<ITicketStore>(context),
        ResolveCookiesLogger(context));
}