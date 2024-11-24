
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public const string CorrelationId = ".xsrf";
  public const string CallbackUri = ".callbackUri";

  public static string? SetAuthPropsCallbackUri(AuthenticationProperties authProps, string callbackUri) => SetAuthPropsItem(authProps, CallbackUri, callbackUri);

  public static string? SetAuthPropsCodeVerifier(AuthenticationProperties authProps, string codeVerifier) => SetAuthPropsItem(authProps, OAuthParamNames.CodeVerifier, codeVerifier);

  public static string? SetAuthPropsCorrelationId(AuthenticationProperties authProps, string correlationId) => SetAuthPropsItem(authProps, CorrelationId, correlationId);
}