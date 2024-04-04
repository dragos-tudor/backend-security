
using System.Net.Http;
using System.Net.Mime;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  const string Bearer = "Bearer";

  public static HttpRequestMessage BuildUserInfoRequest(
    string userInfoEndpoint,
    string accessToken)
  {
    var request = CreateUserInfoRequest(userInfoEndpoint);
    SetUserInfoRequestAcceptType(request, MediaTypeNames.Application.Json);
    SetUserInfoRequestBearer(request, Bearer, accessToken);
    return request;
  }

}