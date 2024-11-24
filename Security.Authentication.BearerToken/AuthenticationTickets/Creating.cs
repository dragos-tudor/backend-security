using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  static AuthenticationTicket CreateBearerTokenTicket(
    ClaimsPrincipal principal,
    AuthenticationProperties authProps,
    BearerTokenOptions tokenOptions) =>
      new(principal, authProps, $"{tokenOptions.SchemeName}:AccessToken");

  static AuthenticationTicket CreateRefreshTicket(
    ClaimsPrincipal principal,
    AuthenticationProperties authProps,
    BearerTokenOptions tokenOptions) =>
      new(principal, authProps, $"{tokenOptions.SchemeName}:RefreshToken");

}