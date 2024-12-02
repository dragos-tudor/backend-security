using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static JwtPayload ParseJsonUserInfoData(string content) => ReadJsonTokenPayload(content);

  static JwtPayload ParseJwtUserInfoData(string content)
  {
    var userInfoJwt = new JwtSecurityToken(content);
    return userInfoJwt.Payload;
  }

  static JwtPayload? ParseUserInfoData(string content, string? contentType)
  {
    if (IsJsonContentTypeHttpResponse(contentType)) return ParseJsonUserInfoData(content);
    if (IsJwtContentTypeHttpResponse(contentType)) return ParseJwtUserInfoData(content);
    return default;
  }
}