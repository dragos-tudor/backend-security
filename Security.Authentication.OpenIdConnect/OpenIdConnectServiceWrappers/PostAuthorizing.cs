
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static Task<PostAuthorizeResult> PostAuthorize<TOptions>(
    HttpContext context)
  where TOptions : OpenIdConnectOptions =>
    PostAuthorize(
      context,
      ResolveRequiredService<TOptions>(context),
      ResolveRequiredService<OpenIdConnectValidationOptions>(context),
      ResolvePropertiesDataFormat(context)
    );
}