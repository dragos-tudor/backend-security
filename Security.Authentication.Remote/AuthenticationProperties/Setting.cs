
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Remote;

partial class RemoteFuncs {

  public const string CallbackUri = "callbackUri";

  public static string SetAuthenticationPropertiesCallbackUri (AuthenticationProperties properties, string callbackUri) =>
    properties.Items[CallbackUri] = callbackUri;

  public static void SetAuthenticationPropertiesCodeVerifier (AuthenticationProperties authProperties, string codeVerifier) =>
    authProperties.Items.Add(CodeVerifier, codeVerifier);

  public static string SetAuthenticationPropertiesCorrelationId (AuthenticationProperties properties, string correlationId) =>
    properties.Items[CorrelationIdKey] = correlationId;

  public static string SetAuthenticationPropertiesRedirectUri (AuthenticationProperties properties, string redirectUri) =>
    properties.RedirectUri ??= redirectUri;

}