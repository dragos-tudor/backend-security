using System.Security.Claims;

namespace Security.Samples;

partial class SampleFuncs
{
  static ClaimsPrincipal CreateUserClaimsPrincipal(HttpContext context) =>
    CreatePrincipal(
      ResolveService<CookieAuthenticationOptions>(context).SchemeName,
      [ CreateNameClaim(context.Request.Form["user"]) ]);

  static string? SignInEndpoint(HttpContext context) =>
    SignInCookie(
      context,
      CreateUserClaimsPrincipal(context),
      CreateAuthenticationProperties()
    ).AuthenticationScheme;

}