
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static string ChallengeOidc<TOptions> (
    HttpContext context,
    AuthenticationProperties authProperties,
    TOptions authOptions)
  where TOptions : OpenIdConnectOptions
  {
    var returnUri = GetChallengeReturnUri(context.Request, authProperties);
    var challengePath = BuildChallengePath(authOptions, returnUri);

    LogChallenged(ResolveOpenIdConnectLogger(context), authOptions.SchemeName, challengePath, context.TraceIdentifier);
    return SetResponseRedirect(context.Response, challengePath)!;
  }

  public static string ChallengeOidc<TOptions> (
    HttpContext context,
    AuthenticationProperties authProperties)
  where TOptions : OpenIdConnectOptions =>
     ChallengeOidc(
      context,
      authProperties,
      ResolveService<TOptions>(context));
}