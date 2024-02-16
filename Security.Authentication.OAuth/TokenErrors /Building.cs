
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  const string TokenErrorDescription = "error_description";
  const string TokenErrorUri = "error_uri";
  internal const string TokenEndpointError = "Token endpoint failure";

  static string BuildTokenErrorFromJson (JsonElement elem) =>
    new StringBuilder(TokenEndpointError)
      .AddErrorDetail("Description", elem.GetString(TokenErrorDescription))
      .AddErrorDetail("Uri", elem.GetString(TokenErrorUri))
      .ToString();

  static string BuildTokenErrorFromResponse (HttpResponseMessage response, string responseContent) =>
    $"{TokenEndpointError}. Status: {response.StatusCode}. Headers: {response.Headers}. Body: {responseContent};";
}