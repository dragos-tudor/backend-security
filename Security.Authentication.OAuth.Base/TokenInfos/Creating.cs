
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static TokenInfo CreateTokenInfo(JsonElement elem) =>
    new(
      elem.GetString(OAuthParamNames.AccessToken),
      elem.GetString(OAuthParamNames.RefreshToken),
      elem.GetString(OAuthParamNames.TokenType),
      elem.GetString(OAuthParamNames.ExpiresIn)
    );
}