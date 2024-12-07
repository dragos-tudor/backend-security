using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static Task<bool> ChallengeSignOut<TOptions>(
    HttpContext context,
    AuthenticationProperties authProps,
    ILogger logger)
  where TOptions : OpenIdConnectOptions =>
    ChallengeSignOut(
      context,
      authProps,
      ResolveRequiredService<TOptions>(context),
      ResolvePropertiesDataFormat(context),
      logger);
}