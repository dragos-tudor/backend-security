
namespace Security.Samples;

partial class Funcs {

  static AuthenticateResult AuthenticateLocal (HttpContext context) =>
    AuthenticateCookie(
      context,
      ResolveService<CookieAuthenticationOptions>(context),
      ResolveService<CookieBuilder>(context),
      ResolveService<TimeProvider>(context).GetUtcNow());

}