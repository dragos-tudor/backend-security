
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  const string AccessToken = "access_token";
  const string ExpiresIn = "expires_in";
  const string RefreshToken = "refresh_token";
  const string TokenType = "token_type";

  internal static TokenResult CreateFailureTokenResult (string error) =>
    new (default, error);

  internal static TokenResult CreateSuccessTokenResult (JsonElement elem) =>
    new (
      new TokenInfo(
        elem.GetString(AccessToken),
        elem.GetString(TokenType),
        elem.GetString(RefreshToken),
        elem.GetString(ExpiresIn)),
      default
    );
}