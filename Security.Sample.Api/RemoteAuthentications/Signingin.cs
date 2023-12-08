
using System.Security.Claims;

namespace Security.Samples;

partial class Funcs {

  static AuthenticationTicket SignInRemote (
    HttpContext context,
    ClaimsPrincipal principal,
    AuthenticationProperties authProperties) =>
      SignInCookie(
        context,
        principal,
        authProperties,
        ResolveService<CookieAuthenticationOptions>(context),
        ResolveService<CookieBuilder>(context),
        ResolveService<TimeProvider>(context).GetUtcNow()
      );

}