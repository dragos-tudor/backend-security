
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static string? GetAuthPropsRedirectUriForCode(AuthenticationProperties authProps) => GetAuthPropsItem(authProps, OAuthParamNames.RedirectUriForCodeProperties);

  public static string? GetAuthPropsCodeVerifier(AuthenticationProperties authProps) => GetAuthPropsItem(authProps, OAuthParamNames.CodeVerifier);

  public static string? GetAuthPropsCorrelationId(AuthenticationProperties authProps) => GetAuthPropsItem(authProps, CorrelationId);
}