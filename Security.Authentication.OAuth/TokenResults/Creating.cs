
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  const string AccessToken = "access_token";
  const string ExpiresIn = "expires_in";
  const string RefreshToken = "refresh_token";
  const string TokenType = "token_type";

  static TokenResult CreateFailureTokenResult (string error) =>
    new (default, error);

  static TokenResult CreateSuccessTokenResult (JsonElement elem) =>
    new (new TokenInfo() {
      AccessToken = elem.GetString(AccessToken),
      ExpiresIn = elem.GetString(ExpiresIn),
      RefreshToken = elem.GetString(RefreshToken),
      TokenType = elem.GetString(TokenType)
    }, default);

}