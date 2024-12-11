
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public const string CorrelationId = ".xsrf";
  public const string CallbackUri = ".callbackUri";

  public static string SetAuthPropsCallbackUri(AuthenticationProperties authProps, string callbackUri) => SetAuthPropsItem(authProps, CallbackUri, callbackUri);

  public static string SetAuthPropsCodeVerifier(AuthenticationProperties authProps, string codeVerifier) => SetAuthPropsItem(authProps, OAuthParamNames.CodeVerifier, codeVerifier);

  public static string SetAuthPropsCorrelationId(AuthenticationProperties authProps, string correlationId) => SetAuthPropsItem(authProps, CorrelationId, correlationId);

  public static AuthenticationProperties SetAuthPropsTokens(AuthenticationProperties authProps, OAuthTokens tokens)
  {
    if (IsNotEmptyString(tokens.TokenType)) SetAuthPropsItem(authProps, OAuthParamNames.TokenType, tokens.TokenType!);
    if (IsNotEmptyString(tokens.AccessToken)) SetAuthPropsItem(authProps, OAuthParamNames.AccessToken, tokens.AccessToken!);
    if (IsNotEmptyString(tokens.RefreshToken)) SetAuthPropsItem(authProps, OAuthParamNames.RefreshToken, tokens.RefreshToken!);
    return authProps;
  }
}