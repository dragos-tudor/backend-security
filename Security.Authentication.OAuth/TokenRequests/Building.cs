
using System.Net.Http;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static HttpRequestMessage BuildTokenRequest(
    OAuthOptions authOptions,
    OAuthParams tokenParams,
    Version requestVersion) =>
      SetTokenRequest(CreateHttpPostRequest(authOptions.TokenEndpoint), tokenParams, requestVersion);
}