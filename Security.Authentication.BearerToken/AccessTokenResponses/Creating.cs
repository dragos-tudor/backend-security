using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  static AccessTokenResponse CreateAccessTokenResponse(
    AuthenticationTicket bearerTokenTicket,
    AuthenticationTicket refreshTokenTicket,
    BearerTokenOptions tokenOptions,
    BearerTokenProtector bearerTokenProtector,
    RefreshTokenProtector refreshTokenProtector)
    =>
      new (){
        AccessToken = bearerTokenProtector.Protect(bearerTokenTicket),
        ExpiresIn = (long)tokenOptions.BearerTokenExpiration.TotalSeconds,
        RefreshToken = refreshTokenProtector.Protect(refreshTokenTicket),
      };
}