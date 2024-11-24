
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static Task<AuthorizationResult> PostAuthorization<TOptions>(
    HttpContext context)
  where TOptions : OpenIdConnectOptions =>
      PostAuthorization(
        context,
        ResolveRequiredService<TOptions>(context),
        ResolvePropertiesDataFormat(context),
        ResolveStringDataFormat(context)
      );
}