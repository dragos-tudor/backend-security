
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static async ValueTask<string?> SetChallengeResponse(
    HttpContext context,
    OpenIdConnectMessage authMessage,
    OpenIdConnectOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration)
  {
    if (IsRedirectGetOpenIdConnectAuthenticationMethod(oidcOptions))
      return SetResponseRedirect(context.Response, authMessage.CreateAuthenticationRequestUrl());

    if (IsFormPostOpenIdConnectAuthenticationMethod(oidcOptions))
    {
      ResetResponseCacheHeaders(context.Response);
      await WriteResponseTextContent(context.Response, authMessage.BuildFormPost(), context.RequestAborted);
      return SetResponseRedirect(context.Response, oidcConfiguration.AuthorizationEndpoint);
    }

    return default;
  }
}