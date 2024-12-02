
using System.IdentityModel.Tokens.Jwt;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static string GetJwtTokenAlgorithm(JwtSecurityToken jwtToken) => (jwtToken.InnerToken ?? jwtToken).Header.Alg;

  public static string GetJwtTokenPayloadSub(JwtSecurityToken jwtToken) => jwtToken.Payload.Sub;

  public static string GetJwtTokenPayloadSub(JwtPayload jwtPayload) => jwtPayload.Sub;
}