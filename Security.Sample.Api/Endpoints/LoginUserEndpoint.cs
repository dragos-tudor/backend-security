using System.Security.Claims;

namespace Security.Samples;

partial class SampleFuncs
{
  const string LoginUserEndpointName = "/login";

  static ClaimsPrincipal CreateUserClaimsPrincipal(HttpContext context) =>
    CreateClaimsPrincipal(
      ResolveService<CookieAuthenticationOptions>(context).SchemeName,
      [ CreateNameClaim(context.Request.Form["user"]) ]);

  static string? LoginUserEndpoint(HttpContext context) =>
    SignInCookie(
      context,
      CreateUserClaimsPrincipal(context),
      CreateAuthenticationProperties()
    ).AuthenticationScheme;

}