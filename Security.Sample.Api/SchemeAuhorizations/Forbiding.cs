
namespace Security.Samples;

partial class Funcs {

  static string? ForbidScheme (HttpContext context, string? schemeName = default) =>
    ForbidCookie(
      context,
      CreateAuthenticationProperties(),
      ResolveService<CookieAuthenticationOptions>(context));

}