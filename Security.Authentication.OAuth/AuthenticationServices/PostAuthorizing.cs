
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static (AuthenticationProperties?, string?) PostAuthorize<TOptions> (
    HttpContext context,
    TOptions authOptions,
    ISecureDataFormat<AuthenticationProperties> secureDataFormat)
  where TOptions: OAuthOptions
  {
    if (ValidateAuthorizationResult(context) is string authError) {
      if(IsAccessDeniedError(authError))
        SetResponseRedirect(context.Response, authOptions.AccessDeniedPath);
      return (default, authError);
    }
    if (UnprotectAuthenticationProperties(GetAuthorizationState(context.Request), secureDataFormat) is not AuthenticationProperties authProperties) return (default, UnprotectAuthorizationStateFailed);
    if (ValidateAuthorizationCorrelationCookie(context, authProperties) is string correlationError) return (default, correlationError);

    UnsetupCorrelationCookie(context, authOptions, GetAuthenticationPropertiesCorrelationId(authProperties)!);
    DeleteAuthenticationPropertiesCorrelationId(authProperties);
    return (authProperties, default);
  }

  public static (AuthenticationProperties?, string?) PostAuthorize<TOptions> (HttpContext context)
  where TOptions: OAuthOptions =>
    PostAuthorize(
      context,
      ResolveService<TOptions>(context),
      ResolveService<ISecureDataFormat<AuthenticationProperties>>(context)
    );

}