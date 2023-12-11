
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class Funcs {

  public static (AuthenticationProperties?, string?) PostAuthorize<TOptions> (
    HttpContext context,
    TOptions authOptions) where TOptions: OAuthOptions
  {
    if (ValidateAuthorizationResult(context) is string authError) return (default, authError);
    if (UnprotectAuthenticationProperties(GetAuthorizationState(context.Request), authOptions.StateDataFormat) is not AuthenticationProperties authProperties) return (default, UnprotectAuthorizationStateFailed);
    if (ValidateAuthorizationCorrelationCookie(context, authProperties) is string correlationError) return (default, correlationError);

    UnsetupCorrelationCookie(context, authOptions, GetAuthenticationPropertiesCorrelationId(authProperties)!);
    DeleteAuthenticationPropertiesCorrelationId(authProperties);
    return (authProperties, default);
  }

}