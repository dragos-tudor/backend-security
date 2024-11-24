
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static AuthorizationResult PostAuthorization<TOptions>(
    HttpContext context)
  where TOptions: OAuthOptions =>
    PostAuthorization(
      context,
      ResolveRequiredService<TOptions>(context),
      ResolvePropertiesDataFormat(context)
    );
}