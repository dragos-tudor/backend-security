
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
#nullable disable

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static PostAuthorizationResult PostAuthorization<TOptions> (
    HttpContext context,
    TOptions authOptions,
    PropertiesDataFormat propertiesDataFormat)
  where TOptions: OAuthOptions
  {
    var authError = ValidatePostAuthorizationRequest(context);
    if (ExistsPostAuthorizationValidationError(authError)) return authError;

    var authProperties = UnprotectAuthenticationProperties(GetPostAuthorizationState(context.Request)!, propertiesDataFormat);
    if (!ExistAuthenticationProperties(authProperties)) return UnprotectAuthorizationStateFailed;

    var correlationError = ValidateCorrelationCookie(context.Request, authProperties);
    if (ExistsCorrelationCookieValidationError(correlationError)) return correlationError;

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
      ResolveRequiredService<TOptions>(context),
      ResolvePropertiesDataFormat(context)
    );
}