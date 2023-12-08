
namespace Security.Samples;

partial class Funcs {

  static string? ChallengeScheme (HttpContext context, string? schemeName = default) =>
    ChallengeCookie(
      context,
      CreateAuthenticationProperties(),
      ResolveService<CookieAuthenticationOptions>(context));

}