using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static JsonDocument ParseJsonUserInfoData(string content) =>
    JsonDocument.Parse(content);

  static JsonDocument ParseJwtUserInfoData(string content)
  {
    var userInfoJwt = new JwtSecurityToken(content);
    var userInfoJson = userInfoJwt.Payload.SerializeToJson();
    return JsonDocument.Parse(userInfoJson);
  }

  static JsonDocument? ParseUserInfoData(string content, string? contentType)
  {
    if (IsJsonContentTypeUserInfoResponse(contentType))
      return ParseJsonUserInfoData(content);

    if (IsJwtContentTypeUserInfoResponse(contentType))
      return ParseJwtUserInfoData(content);

    return default;
  }
}