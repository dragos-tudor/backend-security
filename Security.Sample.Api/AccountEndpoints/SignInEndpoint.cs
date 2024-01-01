using System.Security.Claims;
using System.Threading.Tasks;

namespace Security.Samples;

partial class SampleFuncs
{
  static ClaimsPrincipal CreateUserClaimsPrincipal(HttpContext context) =>
    CreatePrincipal(
      ResolveService<CookieAuthenticationOptions>(context).SchemeName,
      [ CreateNameClaim(context.Request.Form["user"]) ]);

  static async Task<string?> SignInEndpoint(HttpContext context) =>
    (await SignInCookie(
      context,
      CreateUserClaimsPrincipal(context),
      CreateAuthenticationProperties()
    )).AuthenticationScheme;

}