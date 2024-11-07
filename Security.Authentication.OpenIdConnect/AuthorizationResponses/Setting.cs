
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static async ValueTask<string?> SetAuthorizationResponse(
    HttpContext context,
    OpenIdConnectMessage oidcMessage,
    OpenIdConnectOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration)
  {
    if (IsRedirectGetOpenIdConnectAuthenticationMethod(oidcOptions))
      return SetResponseRedirect(context.Response, oidcMessage.CreateAuthenticationRequestUrl());

    if (IsFormPostOpenIdConnectAuthenticationMethod(oidcOptions))
    {
      ResetResponseCacheHeaders(context.Response);
      await WriteResponseTextContent(context.Response, oidcMessage.BuildFormPost(), context.RequestAborted);
      return oidcConfiguration.AuthorizationEndpoint;
    }

    return default;
  }
}