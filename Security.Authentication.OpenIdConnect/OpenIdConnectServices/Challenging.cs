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

    UseCorrelationCookie(context, GenerateCorrelationId(), oidcOptions, currentUtc);

    if (ShouldUseNonceCookie(oidcOptions))
      UseNonceCookie(context, nonceCookieBuilder, stringDataFormat, protocolValidator, currentUtc);

    SetChallengeAuthenticationProperties(authProperties, oidcMessage, GetRequestUrl(context.Request));
    if (ShouldUseCodeChallenge(oidcOptions))
      SetAuthenticationPropertiesCodeVerifier(authProperties, GenerateCodeVerifier());

    SetChallengeOpenIdConnectMessage(oidcMessage, authProperties, oidcOptions, propertiesDataFormat);
    if (ShouldUseCodeChallenge(oidcOptions))
      SetAuthorizationParamsCodeChallenge(oidcMessage.Parameters, authProperties);

    if (IsRedirectGetOpenIdConnectAuthenticationMethod(oidcOptions))
      SetResponseRedirect(context.Response, oidcMessage.CreateAuthenticationRequestUrl());

    if (IsFormPostOpenIdConnectAuthenticationMethod(oidcOptions))
    {
      ResetResponseCacheHeaders(context.Response);
      await WriteResponseHtmlContent(context.Response, oidcMessage.BuildFormPost());
    }

    SanitizeChallengeHttpResponse(context.Response);
    return GetResponseLocation(context.Response);
  }

  public static ValueTask<string?> ChallengeOidc(
    HttpContext context,
    AuthenticationProperties authProperties) =>
      ChallengeOidc(
        context,
        authProperties,
        ResolveService<OpenIdConnectOptions>(context),
        ResolveService<OpenIdConnectConfiguration>(context),
        ResolveService<NonceCookieBuilder>(context) ?? new NonceCookieBuilder(ResolveService<OpenIdConnectOptions>(context)),
        ResolveService<PropertiesDataFormat>(context),
        ResolveService<StringDataFormat>(context),
        ResolveService<OpenIdConnectProtocolValidator>(context) ?? new OpenIdConnectProtocolValidator(),
        ResolveService<TimeProvider>(context).GetUtcNow()
      );
}