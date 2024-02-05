
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
    var authError = ValidateAuthorizationResult(context);
    if (IsAccessDeniedError(authError)) SetResponseRedirect(context.Response, authOptions.AccessDeniedPath);
    if (authError is not null) return (default, authError);

    var authProperties = UnprotectAuthenticationProperties(GetAuthorizationState(context.Request), propertiesDataFormat);
    if (authProperties is null) return (default, UnprotectAuthorizationStateFailed);

    var correlationError = ValidateAuthorizationCorrelationCookie(context, authProperties);
    if (correlationError is not null) return (default, correlationError);

    var correlationId = GetAuthenticationPropertiesCorrelationId(authProperties);
    CleanCorrelationCookie(context, authOptions, correlationId);
    RemoveAuthenticationPropertiesCorrelationId(authProperties);

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