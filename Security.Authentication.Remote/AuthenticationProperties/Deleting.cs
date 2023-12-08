
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Remote;

partial class Funcs {

  public static bool DeleteAuthenticationPropertiesCorrelationId (AuthenticationProperties properties) =>
    properties.Items.Remove(CorrelationIdKey);

  public static string? DeleteAuthenticationPropertiesRedirectUri (AuthenticationProperties properties) =>
    properties.RedirectUri = default;

}