
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static async ValueTask<string> SetChallengeSignoutResponse(
    HttpContext context,
    OpenIdConnectMessage oidcMessage,
    OpenIdConnectOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration)
  {
    if (IsRedirectGetAuthMethod(oidcOptions))
      return SetHttpResponseRedirect(context.Response, oidcMessage.CreateLogoutRequestUrl())!;

    if (IsFormPostAuthMethod(oidcOptions))
    {
      ResetHttpResponseCacheHeaders(context.Response);
      await WriteHttpResponseTextContent(context.Response, oidcMessage.BuildFormPost(), context.RequestAborted);
      return oidcConfiguration.EndSessionEndpoint;
    }

    return HomeUri;
  }
}