
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Remote;

partial class Funcs {

  internal const string CorrelationIdKey = ".xsrf";

  public static string? GetAuthenticationPropertiesCorrelationId (AuthenticationProperties properties) =>
    GetAuthenticationPropertiesItem(properties, CorrelationIdKey);

  public static string? GetAuthenticationPropertiesItem (AuthenticationProperties properties, string key) =>
    ExistsAuthenticationPropertyItem(properties, key) ? properties.Items[key] : default;

  public static string? GetAuthenticationPropertiesRedirectUri (AuthenticationProperties? properties) =>
    properties?.RedirectUri;

}