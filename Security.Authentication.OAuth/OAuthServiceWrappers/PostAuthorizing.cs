
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static PostAuthorizeResult PostAuthorize<TOptions>(
    HttpContext context)
  where TOptions: OAuthOptions =>
    PostAuthorize(
      context,
      ResolveRequiredService<TOptions>(context),
      ResolvePropertiesDataFormat(context)
    );
}