using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static JwtPayload ReadJwtUserInfoResponse(string content) => new JwtSecurityToken(content).Payload;

  static JwtPayload ReadUserInfoToken(string content, string? contentType)
  {
    if (IsJsonContentTypeHttpResponse(contentType)) return ReadJsonTokenPayload(content);
    if (IsJwtContentTypeHttpResponse(contentType)) return ReadJwtUserInfoResponse(content);
    return [];
  }

  public static async Task<JwtPayload> ReadUserInfoToken(HttpResponseMessage response, CancellationToken cancellationToken = default)
  {
    var content = await ReadHttpResponseContent(response, cancellationToken);
    var contentType = GetHttpResponseContentType(response);
    return ReadUserInfoToken(content, contentType);
  }
}