
using System.IdentityModel.Tokens.Jwt;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static JwtPayload ReadJsonTokenPayload(string token)
  {
    var handler = new JwtSecurityTokenHandler();

    if (handler.CanReadToken(token))
      return (handler.ReadToken(token) as JwtSecurityToken)!.Payload;

    return JwtPayload.Deserialize(token);
  }
}