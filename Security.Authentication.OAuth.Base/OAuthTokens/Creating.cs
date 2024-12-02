
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static OAuthTokens CreateOAuthTokens(JsonElement data) =>
    new(
      data.GetString(OAuthParamNames.AccessToken),
      data.GetString(OAuthParamNames.RefreshToken),
      data.GetString(OAuthParamNames.TokenType),
      data.GetString(OAuthParamNames.ExpiresIn)
    );
}