using System.IdentityModel.Tokens.Jwt;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static JwtPayload ReadJwtUserInfoResponse(string content) => new JwtSecurityToken(content).Payload;

  static JwtPayload ReadUserInfoToken(string content, string contentType)
  {
    if (IsJsonContentTypeHttpResponse(contentType)) return ReadJsonTokenPayload(content);
    if (IsJwtContentTypeHttpResponse(contentType)) return ReadJwtUserInfoResponse(content);
    return [];
  }
}