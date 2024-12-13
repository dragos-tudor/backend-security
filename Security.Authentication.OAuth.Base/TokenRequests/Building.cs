
using System.Net.Http;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static HttpRequestMessage BuildTokenRequest(
    OAuthOptions oauthOptions,
    OAuthParams tokenParams,
    Version requestVersion) =>
      SetTokenRequest(CreateHttpPostRequest(oauthOptions.TokenEndpoint), tokenParams, requestVersion);
}