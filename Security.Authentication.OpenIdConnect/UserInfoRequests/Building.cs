
using System.Net.Http;
using System.Net.Mime;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  const string Bearer = "Bearer";

  public static HttpRequestMessage BuildUserInfoRequest(
    string userInfoEndpoint,
    string accessToken,
    Version version)
  {
    var request = CreateUserInfoRequest(userInfoEndpoint);

    SetUserInfoRequestAcceptType(request, MediaTypeNames.Application.Json);
    SetUserInfoRequestBearer(request, Bearer, accessToken);
    SetUserInfoRequestVersion(request, version);

    return request;
  }
}