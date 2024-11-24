using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static Task<string?> ChallengeSignoutOidc<TOptions>(
    HttpContext context,
    AuthenticationProperties authProps,
    ILogger logger)
  where TOptions : OpenIdConnectOptions =>
    ChallengeSignoutOidc(
      context,
      authProps,
      ResolveRequiredService<TOptions>(context),
      ResolveRequiredService<OpenIdConnectConfiguration>(context),
      ResolvePropertiesDataFormat(context),
      logger);
}