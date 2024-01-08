using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  static AuthenticationTicket CreateBearerTicket(
    ClaimsPrincipal user,
    AuthenticationProperties authProperties,
    BearerTokenOptions tokenOptions) =>
      new(user, authProperties, $"{tokenOptions.SchemeName}:AccessToken");

  static AuthenticationTicket CreateRefreshTicket(
    ClaimsPrincipal user,
    BearerTokenOptions tokenOptions,
    DateTimeOffset currentUtc)
  {
    var authProperties = new AuthenticationProperties();
    SetAuthenticationPropertiesExpires(authProperties, currentUtc, tokenOptions.BearerTokenExpiration);
    return new AuthenticationTicket(user, authProperties, $"{tokenOptions.SchemeName}:RefreshToken");
  }
}