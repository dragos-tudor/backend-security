using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  static AuthenticationTicket CreateBearerTokenTicket(
    ClaimsPrincipal principal,
    AuthenticationProperties authProperties,
    BearerTokenOptions tokenOptions) =>
      new (principal, authProperties, $"{tokenOptions.SchemeName}:AccessToken");

  static AuthenticationTicket CreateRefreshTicket(
    ClaimsPrincipal principal,
    AuthenticationProperties authProperties,
    BearerTokenOptions tokenOptions) =>
      new (principal, authProperties, $"{tokenOptions.SchemeName}:RefreshToken");

}