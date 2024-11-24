
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static string ChallengeOAuth<TOptions>(
    HttpContext context,
    AuthenticationProperties? authProps,
    ILogger logger)
  where TOptions: OAuthOptions =>
    ChallengeOAuth(
      context,
      authProps ?? CreateAuthProps(),
      ResolveRequiredService<TOptions>(context),
      ResolveTimeProvider(context).GetUtcNow(),
      ResolvePropertiesDataFormat(context),
      logger);
}