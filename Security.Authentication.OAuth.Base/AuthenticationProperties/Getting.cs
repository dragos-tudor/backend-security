
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static string? GetAuthPropsCallbackUri(AuthenticationProperties authProps) => GetAuthPropsItem(authProps, CallbackUri);

  public static string? GetAuthPropsCodeVerifier(AuthenticationProperties authProps) => GetAuthPropsItem(authProps, OAuthParamNames.CodeVerifier);

  public static string? GetAuthPropsCorrelationId(AuthenticationProperties authProps) => GetAuthPropsItem(authProps, CorrelationId);
}