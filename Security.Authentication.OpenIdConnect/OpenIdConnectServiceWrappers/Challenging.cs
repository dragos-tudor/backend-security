
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static ValueTask<string?> ChallengeOidc<TOptions>(
    HttpContext context,
    AuthenticationProperties? authProps,
    ILogger logger)
  where TOptions : OpenIdConnectOptions =>
      ChallengeOidc(
        context,
        authProps ?? CreateAuthProps(),
        ResolveRequiredService<TOptions>(context),
        ResolveTimeProvider(context).GetUtcNow(),
        ResolvePropertiesDataFormat(context),
        ResolveStringDataFormat(context),
        logger);
}