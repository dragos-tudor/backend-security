
using System.Net.Http;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static HttpRequestMessage BuildUserInfoRequest(
    string requestUri,
    string accessToken) =>
      SetUserInfoRequest(CreateHttpGetRequest(requestUri), accessToken);
}