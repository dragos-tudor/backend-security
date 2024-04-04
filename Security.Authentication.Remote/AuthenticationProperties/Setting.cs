
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  public const string CallbackUri = "callbackUri";

  public static string SetAuthenticationPropertiesCallbackUri (AuthenticationProperties authProperties, string callbackUri) =>
    SetAuthenticationPropertiesItem(authProperties, CallbackUri, callbackUri);

  public static string SetAuthenticationPropertiesCodeVerifier (AuthenticationProperties authProperties, string codeVerifier) =>
    SetAuthenticationPropertiesItem(authProperties, CodeVerifier, codeVerifier);

  public static string SetAuthenticationPropertiesCorrelationId (AuthenticationProperties authProperties, string correlationId) =>
    SetAuthenticationPropertiesItem(authProperties, CorrelationIdKey, correlationId);
}