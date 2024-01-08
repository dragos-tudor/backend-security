using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  static AccessTokenResponse CreateAccessTokenResponse(
    ClaimsPrincipal user,
    AuthenticationProperties authProperties,
    BearerTokenOptions tokenOptions,
    ISecureDataFormat<AuthenticationTicket> bearerTokenProtector,
    ISecureDataFormat<AuthenticationTicket> refreshTokenProtector,
    DateTimeOffset currentUtc)
    =>
      new (){
        AccessToken = bearerTokenProtector.Protect(CreateBearerTicket(user, authProperties, tokenOptions)),
        ExpiresIn = (long)tokenOptions.BearerTokenExpiration.TotalSeconds,
        RefreshToken = refreshTokenProtector.Protect(CreateRefreshTicket(user, tokenOptions, currentUtc)),
      };
}