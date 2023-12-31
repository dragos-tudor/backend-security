
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static string ChallengeLocalOAuth<TOptions> (
    HttpContext context,
    AuthenticationProperties authProperties,
    TOptions authOptions)
  where TOptions : OAuthOptions
  {
    var returnUri = GetAuthenticationPropertiesRedirectUri(authProperties) ?? BuildRelativeUri(context.Request);
    var challengePath = BuildChallengePath(authOptions, returnUri);

    LogChallenged(Logger, authOptions.SchemeName, challengePath, context.TraceIdentifier);
    return SetResponseRedirect(context.Response, challengePath);
  }

  public static string ChallengeLocalOAuth<TOptions> (
    HttpContext context,
    AuthenticationProperties authProperties)
  where TOptions : OAuthOptions =>
    ChallengeLocalOAuth(
      context,
      authProperties,
      ResolveService<TOptions>(context));
}