
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static bool ExistsAccessToken(JsonElement data) => IsNotEmptyString(data.GetString(OAuthParamNames.AccessToken));
}