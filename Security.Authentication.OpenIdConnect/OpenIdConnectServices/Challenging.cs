using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static async ValueTask<string?> ChallengeOidc(
    HttpContext context,
    AuthenticationProperties authProperties,
    OpenIdConnectOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration,
    NonceCookieBuilder nonceCookieBuilder,
    PropertiesDataFormat propertiesDataFormat,
    StringDataFormat stringDataFormat,
    OpenIdConnectProtocolValidator protocolValidator,
    DateTimeOffset currentUtc)
  {
    var oidcMessage = CreateOpenIdConnectMessage(context, authProperties, oidcOptions, oidcConfiguration);

    if (IsEmptyString(authProperties.RedirectUri))
      SetAuthenticationPropertiesRedirectUri(authProperties, GetRequestUrl(context.Request));

    if (oidcOptions.RequireNonce)
      AppendNonceCookie(context.Response,
        GetNonceCookieName(nonceCookieBuilder, protocolValidator, stringDataFormat),
        nonceCookieBuilder.Build(context, currentUtc));

    if (oidcOptions.UsePkce && IsCodeOpenIdConnectResponseType(oidcOptions)) {
      var codeVerifier = GenerateCodeVerifier();
      SetAuthenticationPropertiesCodeVerifier(authProperties, codeVerifier);
      AddAuthorizationCodeChallengeParams(oidcMessage.Parameters, HashCodeVerifier(codeVerifier));
    }

    AppendCorrelationCookie(context.Response,
      GetCorrelationCookieName(GenerateCorrelationId()),
      BuildCorrelationCookie(context, oidcOptions, currentUtc));

    if (!IsEmptyString(oidcMessage.State))
      SetAuthenticationPropertiesUserState(authProperties, oidcMessage.State);
    SetAuthenticationPropertiesRedirectUriForCode(authProperties, oidcMessage.RedirectUri);

    if(!IsCodeOpenIdConnectResponseType(oidcOptions) || !IsQueryOpenIdConnectResponseMode(oidcOptions))
      SetOpenIdConnectMessageResponseMode(oidcMessage, oidcOptions.ResponseMode);
    SetOpenIdConnectMessageState(oidcMessage, propertiesDataFormat.Protect(authProperties));

    if (IsRedirectGetOpenIdConnectAuthenticationMethod(oidcOptions))
      SetResponseRedirect(context.Response, oidcMessage.CreateAuthenticationRequestUrl());

    if (IsFormPostOpenIdConnectAuthenticationMethod(oidcOptions))
    {
      var content = oidcMessage.BuildFormPost();
      ResetResponseCacheHeaders(context.Response);
      await WriteResponseHtmlContent(context.Response, content);
    }

    if (IsEmptyString(context.Response.Headers.Location))
      SetResponseLocation(context.Response, "(not set)");

    if (IsEmptyString(context.Response.Headers.SetCookie))
      SetResponseLocation(context.Response, "(not set)");

    return GetResponseLocation(context.Response);
  }
}