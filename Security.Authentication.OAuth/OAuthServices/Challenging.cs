
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static string ChallengeOAuth<TOptions> (
    HttpContext context,
    AuthenticationProperties authProperties,
    TOptions authOptions)
  where TOptions : OAuthOptions
  {
    var returnUri = GetChallengeReturnUri(context.Request, authProperties);
    var challengePath = BuildChallengePath(authOptions, returnUri);

    LogChallenged(ResolveOAuthLogger(context), authOptions.SchemeName, challengePath, context.TraceIdentifier);
    return SetResponseRedirect(context.Response, challengePath)!;
  }

  public static string ChallengeOAuth<TOptions> (
    HttpContext context,
    AuthenticationProperties authProperties)
  where TOptions : OAuthOptions =>
    ChallengeOAuth(
      context,
      authProperties,
      ResolveService<TOptions>(context));
}