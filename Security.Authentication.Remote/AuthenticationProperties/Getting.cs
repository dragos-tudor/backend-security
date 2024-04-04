
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Remote;

partial class RemoteFuncs {

  internal const string CorrelationIdKey = ".xsrf";

  public static string? GetAuthenticationPropertiesCallbackUri (AuthenticationProperties authProperties) =>
    GetAuthenticationPropertiesItem(authProperties, CallbackUri);

  public static string? GetAuthenticationPropertiesCodeVerifier (AuthenticationProperties authProperties) =>
    GetAuthenticationPropertiesItem(authProperties, CodeVerifier);

  public static string? GetAuthenticationPropertiesCorrelationId (AuthenticationProperties authProperties) =>
    GetAuthenticationPropertiesItem(authProperties, CorrelationIdKey);
}