using System.Net.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static bool IsSuccessUserInfoResponse(HttpResponseMessage response) =>
    response.IsSuccessStatusCode;
}