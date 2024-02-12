
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Remote;

partial class RemoteFuncs {

  internal const string CorrelationIdKey = ".xsrf";

  public static string? GetAuthenticationPropertiesItem (AuthenticationProperties authProperties, string key) =>
    authProperties.GetString(key);

  public static T? GetAuthenticationPropertiesParam<T>(AuthenticationProperties authProperties, string key) =>
    authProperties.GetParameter<T>(key);

  public static string? GetAuthenticationPropertiesCodeVerifier (AuthenticationProperties authProperties) =>
    GetAuthenticationPropertiesItem(authProperties, CodeVerifier);

  public static string? GetAuthenticationPropertiesCorrelationId (AuthenticationProperties authProperties) =>
    GetAuthenticationPropertiesItem(authProperties, CorrelationIdKey);

  public static string? GetAuthenticationPropertiesRedirectUri (AuthenticationProperties? authProperties) =>
    authProperties?.RedirectUri;
}