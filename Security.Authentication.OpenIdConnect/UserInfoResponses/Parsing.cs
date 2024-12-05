using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static JwtPayload ParseJsonUserInfoResponse(string content) => ReadJsonTokenPayload(content);

  static JwtPayload ParseJwtUserInfoResponse(string content) => new JwtSecurityToken(content).Payload;

  static JwtPayload ParseUserInfoResponse(string content, string? contentType)
  {
    if (IsJsonContentTypeHttpResponse(contentType)) return ParseJsonUserInfoResponse(content);
    if (IsJwtContentTypeHttpResponse(contentType)) return ParseJwtUserInfoResponse(content);
    return new JwtPayload();
  }
}