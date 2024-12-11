
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static PostAuthorizeResult PostAuthorize<TOptions>(
    HttpContext context,
    TOptions oauthOptions,
    PropertiesDataFormat authPropsProtector)
  where TOptions : OAuthOptions
  {
    if (IsOAuthError(context.Request)) return GetOAuthError(context.Request);

    var authError = ValidateAuthorizationResponse(context.Request);
    if (authError is not null) return authError;

    var state = GetAuthorizationState(context.Request)!;
    var authProps = UnprotectAuthProps(state, authPropsProtector);
    if (authProps is null) return UnprotectStateFailed;

    var correlationError = ValidateCorrelationCookie(context.Request, authProps);
    if (correlationError is not null) return correlationError;

    var code = GetAuthorizationCode(context.Request);
    return (authProps, code);
  }
}