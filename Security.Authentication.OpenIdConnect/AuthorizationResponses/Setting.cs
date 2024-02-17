
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static async ValueTask<string?> SetAuthorizationResponse(
    HttpContext context,
    OpenIdConnectMessage oiedMessage,
    OpenIdConnectOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration)
  {
    if (IsRedirectGetOpenIdConnectAuthenticationMethod(oidcOptions))
      return SetResponseRedirect(context.Response, oiedMessage.CreateAuthenticationRequestUrl());

    if (IsFormPostOpenIdConnectAuthenticationMethod(oidcOptions))
    {
      ResetResponseCacheHeaders(context.Response);
      await WriteResponseTextContent(context.Response, oiedMessage.BuildFormPost(), context.RequestAborted);
      return oidcConfiguration.AuthorizationEndpoint;
    }

    return default;
  }
}