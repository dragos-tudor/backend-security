
using System.Net.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  internal const string UserInfoEndpointError = "User info endpoint failure";

  static string BuildUserInfoError (HttpResponseMessage response, string responseContent) =>
    $"{UserInfoEndpointError}. Status: {response.StatusCode}. Headers: {response.Headers}. Body: {responseContent};";

}