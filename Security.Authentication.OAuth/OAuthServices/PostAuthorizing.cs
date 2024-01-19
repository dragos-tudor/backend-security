
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static (AuthenticationProperties?, string?) PostAuthorize<TOptions> (
    HttpContext context,
    TOptions authOptions,
    PropertiesDataFormat propertiesDataFormat)
  where TOptions: OAuthOptions
  {
    if (ValidateAuthorizationResult(context) is string authError) {
      if(IsAccessDeniedError(authError))
        SetResponseRedirect(context.Response, authOptions.AccessDeniedPath);
      return (default, authError);
    }
    if (UnprotectAuthenticationProperties(GetAuthorizationState(context.Request), propertiesDataFormat) is not AuthenticationProperties authProperties)
      return (default, UnprotectAuthorizationStateFailed);
    if (ValidateAuthorizationCorrelationCookie(context, authProperties) is string correlationError)
      return (default, correlationError);

    var correlationId = GetAuthenticationPropertiesCorrelationId(authProperties);
    var cookieOptions = ResetCorrelationCookie(context, authOptions!);
    DeleteCorrelationCookie(context.Response, GetCorrelationCookieName(correlationId!), cookieOptions);
    DeleteAuthenticationPropertiesCorrelationId(authProperties);
    return (authProperties, default);
  }

  public static (AuthenticationProperties?, string?) PostAuthorize<TOptions> (HttpContext context)
  where TOptions: OAuthOptions =>
    PostAuthorize(
      context,
      ResolveService<TOptions>(context),
      ResolveService<PropertiesDataFormat>(context)
    );

}