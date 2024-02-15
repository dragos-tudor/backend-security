
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static PostAuthorizationResult PostAuthorization<TOptions> (
    HttpContext context,
    TOptions authOptions,
    PropertiesDataFormat propertiesDataFormat)
  where TOptions: OAuthOptions
  {
    var authError = ValidatePostAuthorizationRequest(context);
    if (IsAccessDeniedError(authError)) SetResponseRedirect(context.Response, authOptions.AccessDeniedPath);
    if (authError is not null) return authError;

    var authProperties = UnprotectAuthenticationProperties(GetPostAuthorizationState(context.Request)!, propertiesDataFormat);
    if (authProperties is null) return UnprotectAuthorizationStateFailed;

    var correlationError = ValidateCorrelationCookie(context.Request, authProperties);
    if (correlationError is not null) return correlationError;

    var correlationId = GetAuthenticationPropertiesCorrelationId(authProperties);
    CleanCorrelationCookie(context, authOptions, correlationId);
    RemoveAuthenticationPropertiesCorrelationId(authProperties);

    return authProperties;
  }

  public static PostAuthorizationResult PostAuthorization<TOptions> (
    HttpContext context)
  where TOptions: OAuthOptions =>
    PostAuthorization(
      context,
      ResolveService<TOptions>(context),
      ResolvePropertiesDataFormat<TOptions>(context)
    );

}