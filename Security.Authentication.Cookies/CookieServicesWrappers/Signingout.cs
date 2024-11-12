
#nullable disable
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static ValueTask<bool> SignOutCookie(HttpContext context) =>
    SignOutCookie(
      context,
      ResolveRequiredService<AuthenticationCookieOptions>(context),
      ResolveRequiredService<ICookieManager>(context),
      ResolveRequiredService<TicketDataFormat>(context),
      ResolveRequiredService<ITicketStore>(context),
      ResolveCookiesLogger(context));
}