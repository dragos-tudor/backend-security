
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
    ITicketStore ticketStore)
  {
    var cookieOptions = BuildCookieOptions(cookieBuilder, authProperties, context);
    var cookieName = GetCookieName(cookieBuilder, authOptions);
    DeleteAuthenticationCookie(context, cookieManager, cookieName, cookieOptions);

    if (GetSessionTicketId(context) is string ticketId)
      await RemoveSessionTicket(ticketStore, ticketId);

    ResetResponseCacheHeaders(context.Response);
    if (ResolveRedirectUri(context, authProperties, authOptions) is string redirectUri)
      SetResponseRedirect(context.Response, redirectUri);

    LogSignedOutCookie(Logger, authOptions.SchemeName, context.TraceIdentifier);
    return GetResponseLocation(context.Response);
  }

  public static ValueTask<string?> SignOutCookie (
    HttpContext context,
    AuthenticationProperties authProperties) =>
      SignOutCookie(
        context,
        authProperties,
        ResolveService<CookieAuthenticationOptions>(context),
        ResolveService<CookieBuilder>(context),
        ResolveService<ICookieManager>(context),
        ResolveService<ITicketStore>(context)
        );
}