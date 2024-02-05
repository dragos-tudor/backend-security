
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Remote;

partial class RemoteFuncs {

  public const string CallbackUri = "callbackUri";

  public static string SetAuthenticationPropertiesItem (AuthenticationProperties authProperties, string key, string value)
    { authProperties.SetString(key, value); return value; }

  public static T SetAuthenticationPropertiesParam<T> (AuthenticationProperties authProperties, string key, T value)
    { authProperties.SetParameter(key, value); return value; }

  public static string SetAuthenticationPropertiesCallbackUri (AuthenticationProperties authProperties, string callbackUri) =>
    SetAuthenticationPropertiesItem(authProperties, CallbackUri, callbackUri);

  public static string SetAuthenticationPropertiesCodeVerifier (AuthenticationProperties authProperties, string codeVerifier) =>
    SetAuthenticationPropertiesItem(authProperties, CodeVerifier, codeVerifier);

  public static string SetAuthenticationPropertiesCorrelationId (AuthenticationProperties authProperties, string correlationId) =>
    SetAuthenticationPropertiesItem(authProperties, CorrelationIdKey, correlationId);

  public static string SetAuthenticationPropertiesRedirectUri (AuthenticationProperties authProperties, string redirectUri) =>
    authProperties.RedirectUri ??= redirectUri;

}