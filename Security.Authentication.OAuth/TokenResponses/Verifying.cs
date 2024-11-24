
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static bool ExistsAccessToken(JsonElement response) => IsNotEmptyString(response.GetString(OAuthParamNames.AccessToken));
}