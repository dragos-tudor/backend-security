
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static AuthorizationResult PostAuthorization<TOptions>(
    HttpContext context,
    TOptions authOptions,
    PropertiesDataFormat authPropsProtector)
  where TOptions: OAuthOptions
  {
    var authError = ValidateAuthorizationResponse(context.Request);
    if(authError is not null) return authError;

    var authProps = UnprotectAuthProps(GetAuthorizationState(context.Request)!, authPropsProtector);
    if(authProps is null) return UnprotectStateFailed;

    var correlationError = ValidateCorrelationCookie(context.Request, authProps);
    if(correlationError is not null) return correlationError;

    var correlationId = GetAuthPropsCorrelationId(authProps);
    DeleteCorrelationCookie(context, authOptions, correlationId);
    UnsetAuthPropsCorrelationId(authProps);

    return authProps;
  }
}