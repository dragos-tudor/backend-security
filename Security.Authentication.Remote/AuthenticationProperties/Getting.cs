
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Remote;

partial class RemoteFuncs {

  internal const string CorrelationIdKey = ".xsrf";

  public static string? GetAuthenticationPropertiesCorrelationId (AuthenticationProperties properties) =>
    GetAuthenticationPropertiesItem(properties, CorrelationIdKey);

  public static string? GetAuthenticationPropertiesItem (AuthenticationProperties properties, string key) =>
    properties.Items.TryGetValue(key, out string? authProperties)? authProperties: default;

  public static string? GetAuthenticationPropertiesRedirectUri (AuthenticationProperties? properties) =>
    properties?.RedirectUri;

}