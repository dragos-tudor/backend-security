
using System.Net.Http;
using System.Net.Mime;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  const string Bearer = "Bearer";

  public static HttpRequestMessage BuildUserInfoRequest(string requestUri, string accessToken)
  {
    var request = CreateUserInfoRequest(requestUri);
    SetUserInfoRequestAcceptType(request, MediaTypeNames.Application.Json);
    SetUserInfoRequestBearer(request, Bearer, accessToken);
    return request;
  }

}