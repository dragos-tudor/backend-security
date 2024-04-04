
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  const string TokenError = "error";

  static bool IsJsonTokenError (JsonDocument document) =>
    document.RootElement.GetString(TokenError) is not null;
}