
using System.Net.Http;
using System.Net.Mime;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static HttpRequestMessage BuildUserInfoRequest(
    string requestUri,
    string accessToken) =>
      SetUserInfoRequest(CreateHttpGetRequest(requestUri), accessToken);
}